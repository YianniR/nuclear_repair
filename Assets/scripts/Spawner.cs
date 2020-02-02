using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour {
    public int pcId;

    public float secondsPerWidgetDBPoll = 5;

    public Dictionary<string, GameObject> widgets;

    public int widgetSize;

    private float untilNextDBPoll;

    [System.Serializable]
    public struct lol {
        public string name;
        public GameObject widget;
    }
    public List<lol> lols;

    private List<int> instances = new List<int>();

    private Director director;


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

        director = GameObject.FindWithTag("Director").GetComponent<Director>();

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
        if (untilNextDBPoll <= 0.0f && director.readyForSpawning) {
            getWidgets();
            untilNextDBPoll = secondsPerWidgetDBPoll;
        }
    }

    public int numWidgets () {
        return instances.Count;
    }

    // Read in new json widget(s) and instantiate where necessary.
    public void getWidgets () {
        StartCoroutine(Mongo.GetWidgets((JSONObject data) => {
            foreach (JSONObject json in data["_items"].list) {
                // Get info about the (potential) new widget.
                int pcId = (int)json["pcId"].n;
                string type = json["type"].str;
                int instanceId = (int)json["instanceId"].n;
                List<int> dataIds = new List<int>();
                foreach (JSONObject j in json["dataIds"].list) { dataIds.Add((int)j.n); }

                // If the widget is of a known type, and not with an already used instance ID, then spawn it.
                if (instances.Contains(instanceId))
                    continue;

                foreach (lol l in lols) {
                    if (l.name != type) continue;
                    spawn(l.widget, instanceId, pcId, type, dataIds);
                    break;
                }
            }
        }, pcId));
    }

    // Instantiate a new widget.
    public void spawn (GameObject widget, int instanceId, int pcId, string type, List<int> dataIds) {
        instances.Add(instanceId);

        Vector3 spawnLocation = Vector3.up;
        Vector2 spiralOffset = spiral() * widgetSize;
        spawnLocation.x += spiralOffset.x;
        spawnLocation.z += spiralOffset.y;

        GameObject obj = Instantiate(widget, spawnLocation, Quaternion.identity);

        Widget wdg = obj.GetComponent<Widget>();
        wdg.instanceId = instanceId;
        wdg.pcId = pcId;
        wdg.type = type;
        wdg.dataIds = dataIds;
    }

    // Get the next position in the spiral. WARNING: stateful -- just call once per spiral position.
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
