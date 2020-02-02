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

    // Start is called before the first frame update
    void Start () {
        untilNextWidget = 0.0f;

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

        StartCoroutine(Mongo.CreateOrUpdateWidget(JsonUtility.ToJson(r), (string response) => { return; }));
    }

    private int makePcId () {
        if (!spawnedIntroWidget)
            return introWidget.pcId;

        if (firstWidgetsIndex < firstWidgets.Count)
            return firstWidgets[firstWidgetsIndex].pcId;

        return Random.Range(1, 3);
    }

    private string makeWidget () {
        if (!spawnedIntroWidget)
            return introWidget.type;

        if (firstWidgetsIndex < firstWidgets.Count)
            return firstWidgets[firstWidgetsIndex].type;

        return randomChoiceWidgets[Random.Range(0, randomChoiceWidgets.Count)];
    }

    private List<int> makeDataIds () {
        if (!spawnedIntroWidget)
            return introWidget.dataIds;

        if (firstWidgetsIndex < firstWidgets.Count)
            return firstWidgets[firstWidgetsIndex].dataIds;

        return new List<int>();
    }

}
