using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Spawner : MonoBehaviour {
    public string widgetUrl = "localhost";
    public string dataUrl = "localhost";

    public string widgetsPath = "widgets.json";

    public float secondsPerWidgetDBPoll = 5;

    public Dictionary<string, GameObject> widgets;

    private float untilNextDBPoll;

    public int widgetSize;

    [System.Serializable]
    public struct lol {
        public string name;
        public GameObject widget;
    }
    public List<lol> lols;

    private List<int> instances = new List<int>();


    ///// SPIRALS
    enum Direction { UP, RIGHT, DOWN, LEFT };
    private uint currentSideLength;
    private uint currentProgressOnSide;
    private uint completedSidesAtCurrentLength;
    private Direction currentDirection;
    private Vector2 previousPos;
    ///// SLARIPS


    void Start () {
        untilNextDBPoll = secondsPerWidgetDBPoll;

        ///// SPIRALS
        currentSideLength = 0;
        currentProgressOnSide = 1;
        completedSidesAtCurrentLength = 0;
        currentDirection = Direction.UP;
        previousPos = new Vector2(0, 0);
        ///// SLARIPS
    }

    void Update () {
        // Poll for new widgets.
        untilNextDBPoll -= Time.deltaTime;
        if (untilNextDBPoll <= 0.0f) {
            getWidgets();
            untilNextDBPoll = secondsPerWidgetDBPoll;
        }
    }

    public int numWidgets () {
        return instances.Count;
    }

    // Read in new json widget(s) and instantiate where necessary.
    public void getWidgets () {
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

    // Instantiate a new widget.
    public void spawn (int id, GameObject widget, int partnerId) {
        instances.Add(id);

        Vector3 spawnLocation = Vector3.up;
        Vector2 spiralOffset = spiral() * widgetSize;
        spawnLocation.x += spiralOffset.x;
        spawnLocation.z += spiralOffset.y;

        GameObject obj = Instantiate(widget, spawnLocation, Quaternion.identity);

        Widget wdg = obj.GetComponent<Widget>();
        wdg.instanceId = id;
        wdg.partnerId = partnerId;
    }

    public Vector2 spiral () {
        Vector2 offset = new Vector2(0, 0);

        if (currentSideLength == 0) {
            currentSideLength += 1;
            return offset;
        }

        switch (currentDirection) {
            case Direction.UP:
                offset = Vector2.up;
                break;
            case Direction.RIGHT:
                offset = Vector2.right;
                break;
            case Direction.DOWN:
                offset = Vector2.down;
                break;
            case Direction.LEFT:
                offset = Vector2.left;
                break;
        }

        Vector2 nextPosition = previousPos + offset;

        currentProgressOnSide += 1;

        if (currentProgressOnSide > currentSideLength) {
            currentProgressOnSide = 1;
            completedSidesAtCurrentLength += 1;
            switch (currentDirection) {
                case Direction.UP:
                    currentDirection = Direction.RIGHT;
                    break;
                case Direction.RIGHT:
                    currentDirection = Direction.DOWN;
                    break;
                case Direction.DOWN:
                    currentDirection = Direction.LEFT;
                    break;
                case Direction.LEFT:
                    currentDirection = Direction.UP;
                    break;
            }

            if (completedSidesAtCurrentLength >= 2) {
                currentSideLength += 1;
                completedSidesAtCurrentLength = 0;
            }
        }

        previousPos = nextPosition;

        return nextPosition;
    }
}
