using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    public int pcId;
    public float secondsPerNewWidget = 5;

    public List<string> availableWidgets;

    private float untilNextWidget;

    private int instanceCount = 0;


    [System.Serializable]
    private struct Request {
        public int instanceId;
        public int pcId;
        public string type;
        public List<int> dataIds;
    }

    // Start is called before the first frame update
    void Start () {
        untilNextWidget = secondsPerNewWidget;
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
        r.pcId = pcId;
        r.type = availableWidgets[Random.Range(0, availableWidgets.Count)];
        r.dataIds = new List<int>();

        instanceCount += 1;

        StartCoroutine(Mongo.CreateOrUpdateWidget(JsonUtility.ToJson(r), (string response) => { return; }));
    }
}
