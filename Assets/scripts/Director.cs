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


    [System.Serializable]
    private struct Request {
        public int instanceId;
        public int pcId;
        public string type;
        public List<int> dataIds;
    }

    private int widgetChoiceIndex = 0;
    private List<string> startingWidgetSequence = new List<string>{
        "clock", "health", "bigbutton"
    };
    private int pcIdChoiceIndex = 0;
    private List<int> startingPcIdSequence = new List<int>{
        1, 1, 1
    };

    // Start is called before the first frame update
    void Start () {
        untilNextWidget = secondsPerNewWidget;

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

    private void requestNewWidget () {
        Request r;
        r.instanceId = instanceCount;
        /* r.pcId = pcId; */
        r.pcId = nextPcId();
        /* r.type = availableWidgets[Random.Range(0, availableWidgets.Count)]; */
        r.type = nextWidget();
        r.dataIds = new List<int>();

        instanceCount += 1;

        StartCoroutine(Mongo.CreateOrUpdateWidget(JsonUtility.ToJson(r), (string response) => { return; }));
    }

    private int nextPcId () {
        if (pcIdChoiceIndex < startingPcIdSequence.Count) {
            pcIdChoiceIndex += 1;
            return startingPcIdSequence[pcIdChoiceIndex - 1];
        }

        return Random.Range(1, 3);
    }

    private string nextWidget () {
        if (widgetChoiceIndex < startingWidgetSequence.Count) {
            widgetChoiceIndex += 1;
            return startingWidgetSequence[widgetChoiceIndex - 1];
        }

        return randomChoiceWidgets[Random.Range(0, randomChoiceWidgets.Count)];
    }
}
