using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Spawner : MonoBehaviour {
    public string widgetUrl = "localhost";
    public string dataUrl = "localhost";

    public string widgetsPath = "widgets.json";
    public string dataPath = "data.json";

    public float secondsPerWidgetDBPoll = 5;

    public Dictionary<string, GameObject> widgets;

    private float untilNextDBPoll;

    [System.Serializable]
    public struct lol {
        public string name;
        public GameObject widget;
    }
    public List<lol> lols;

    private List<int> instances;

    // TODO:
    // * [x] read from json (file for now).
    // * [x] store the instantiable widgets.
    // * [x] query the instantiable widgets from the received json.
    // * [x] instantiate a widget from the json params.
    // * [ ] manage the grid of current widgets, so that new ones don't get put
    //       over existing ones.

    void Start () {
        untilNextDBPoll = secondsPerWidgetDBPoll;
        instances = new List<int>();
    }

    void Update () {
        // Poll for new widgets.
        untilNextDBPoll -= Time.deltaTime;
        if (untilNextDBPoll <= 0) {
            getWidgets();
            untilNextDBPoll = secondsPerWidgetDBPoll;
        }
    }

    public void getWidgets () {
        /* UnityWebRequest www = UnityWebRequest.Get(widgetUrl); */
        /* Debug.Log("got '" + www.downloadHandler.text + "' from the DB"); */
        string filePath = widgetsPath.Replace(".json", "");
        string jsonText = Resources.Load<TextAsset>(filePath).text;

        // Get info about the (potential) new widget.
        JSONObject json = new JSONObject(jsonText);
        string typename = json["type"].ToString();
        int id = System.Int32.Parse(json["instanceId"].ToString());
        int partnerId = System.Int32.Parse(json["partnerId"].ToString());

        // If the widget is of a known type, and not with an already used instance ID, then spawn it.
        if (instances.Contains(id)) return;

        foreach (lol l in lols) {
            if (l.name != typename) continue;
            spawn(id, l.widget, partnerId);
            return;
        }
        Debug.Log("Unknown entity type '" + typename + "'!");
    }

    public void spawn (int id, GameObject widget, int partnerId) {
        instances.Add(id);
        GameObject obj = Instantiate(widget, Vector3.up, Quaternion.identity);
        Widget wdg = obj.GetComponent<Widget>();
        wdg.instanceId = id;
        wdg.partnerId = partnerId;
    }
}
