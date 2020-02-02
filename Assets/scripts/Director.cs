using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    public int pcId;
    public float secondsPerNewWidget = 5;

    public List<string> randomChoiceWidgets;

    private float untilNextWidget;

    private int instanceCount = 0;

    public bool readyForSpawning { get; private set; } = false;

    private bool startedPeriodicSpawning = false;


    [System.Serializable]
    private struct Request {
        public int instanceId;
        public int pcId;
        public string type;
        public List<int> dataIds;

        public Request (int instanceId, int pcId, string type, List<int> dataIds) {
            this.instanceId = instanceId;
            this.pcId = pcId;
            this.type = type;
            this.dataIds = dataIds;
        }
    }

    // For guaranteed (ie. not randomly chosen) widgets.
    private struct BakedWidget {
        public int pcId;
        public string type;
        public List<int> dataIds;

        public BakedWidget (int pcId, string type, List<int> dataIds) {
            this.pcId = pcId;
            this.type = type;
            this.dataIds = dataIds;
        }

        public BakedWidget (int pcId, string type) {
            this.pcId = pcId;
            this.type = type;
            this.dataIds = new List<int>();
        }
    }

    private BakedWidget introWidget = new BakedWidget(1, "start");
    private bool spawnedIntroWidget = false;

    private List<BakedWidget> firstWidgets = new List<BakedWidget>{
        new BakedWidget(1, "clock"),
        new BakedWidget(1, "health"),
        new BakedWidget(1, "bigbutton")
    };
    private int firstWidgetsIndex = 0;

    public List<string> onlySpawnOneOf = new List<string>();
    public HashSet<string> spawnableTypes = new HashSet<string>();


    // Start is called before the first frame update
    void Start () {
        untilNextWidget = 0.0f;

        foreach (string s in randomChoiceWidgets) {
            spawnableTypes.Add(s);
        }

        StartCoroutine(Mongo.DeleteEverything());
        readyForSpawning = true;  // callback lol
    }

    // Update is called once per frame
    void Update () {
        // Poll for new widgets.
        untilNextWidget -= Time.deltaTime;
        if (untilNextWidget <= 0.0f) {
            requestNewWidget();
            untilNextWidget = secondsPerNewWidget;
        }
    }

    // Start the spawn timer.
    public void startSpawning () {
        if (startedPeriodicSpawning)
            return;
        startedPeriodicSpawning = true;
        requestNewWidget();
        untilNextWidget = secondsPerNewWidget;
    }

    private void requestNewWidget () {
        if (spawnedIntroWidget && !startedPeriodicSpawning)
            return;

        Request r;
        r.instanceId = instanceCount;
        r.pcId = makePcId();
        r.type = makeWidget();
        r.dataIds = makeDataIds();

        if (spawnedIntroWidget && firstWidgetsIndex < firstWidgets.Count)
            firstWidgetsIndex += 1;

        if (!spawnedIntroWidget)
            spawnedIntroWidget = true;

        instanceCount += 1;

        if (r.type == "radarscreen")
            r.pcId = 1;

        StartCoroutine(Mongo.CreateOrUpdateWidget(JsonUtility.ToJson(r), (string response) => { return; }));
    }

    private int makePcId () {
        if (!spawnedIntroWidget)
            return introWidget.pcId;

        if (firstWidgetsIndex < firstWidgets.Count)
            return firstWidgets[firstWidgetsIndex].pcId;

        return Random.Range(1, 3);
        // return 1;
    }

    private string makeWidget () {
        if (!spawnedIntroWidget)
            return introWidget.type;

        if (firstWidgetsIndex < firstWidgets.Count)
            return firstWidgets[firstWidgetsIndex].type;

        // Get a random element of the set.
        int idx = Random.Range(0, spawnableTypes.Count);
        int size = spawnableTypes.Count;
        int i = 0;
        string choice = "";
        foreach (string str in spawnableTypes) {
            if (i == idx) {
                choice = str;
                break;
            } else {
                i++;
            }
        }

        if (onlySpawnOneOf.Contains(choice))
            spawnableTypes.Remove(choice);

        return choice;
    }

    private List<int> makeDataIds () {
        if (!spawnedIntroWidget)
            return introWidget.dataIds;

        if (firstWidgetsIndex < firstWidgets.Count)
            return firstWidgets[firstWidgetsIndex].dataIds;

        return new List<int>();
    }

}
