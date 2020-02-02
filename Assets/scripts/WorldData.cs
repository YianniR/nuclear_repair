using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour {
    public static float secondsPerPoll = 0.2f;
    public float pollTimeoutDeadline = 1.5f;

    private static float HealthLostOnCycle;

    private float untilNextPoll;
    private float untilPollTimeoutDeadline;

    private Dictionary<int, JSONObject> data;

    private bool readyToPoll;


    void Awake () {
        untilNextPoll = secondsPerPoll;
        readyToPoll = true;
    }

    void FixedUpdate () {
        // If the previous poll took too long, just mark ourselves as ready
        // again.
        untilPollTimeoutDeadline -= Time.deltaTime;
        if (!readyToPoll && untilPollTimeoutDeadline <= 0.0f) {
            readyToPoll = true;
        }

        // Poll for new data, if we should.
        untilNextPoll -= Time.deltaTime;
        if (untilNextPoll <= 0.0f && readyToPoll) {
            poll();
            untilNextPoll = secondsPerPoll;
        }
    }

    // Get some data from the world.
    public JSONObject get (int dataId) {
        return data[dataId];
    }

    // Push some data to the world.
    public void setJson (JSONObject data, int dataId) {
        JSONObject toSubmit = new JSONObject(JSONObject.Type.OBJECT);
        toSubmit.AddField("dataId", dataId);
        toSubmit.AddField("body", data);

        Debug.Log("About to submit " + toSubmit.ToString());
        StartCoroutine(Mongo.CreateOrUpdateData(toSubmit.ToString(), (string res) => { return; }));
    }

    // Push some data from a serializable struct.
    public void set<T> (T data, int dataId) {
        if (!typeof(T).IsSerializable) {
            Debug.Log("Data to set must be [System.Serializable]!");
            return;
        }
        string dataText = JsonUtility.ToJson(data);
        JSONObject dataBody = new JSONObject(dataText);

        setJson(dataBody, dataId);
    }

    private void poll () {
        readyToPoll = false;
        untilPollTimeoutDeadline = pollTimeoutDeadline;
        StartCoroutine(Mongo.GetData((JSONObject res) => {
            // Completely construct the new data dictionary, *then* replace our
            // existing one with it.
            Dictionary<int, JSONObject> newData = new Dictionary<int, JSONObject>();
            foreach (JSONObject json in res["_items"].list) {
                newData[(int)json["dataId"].n] = json["body"] != null ? json["body"] : new JSONObject();
            }

            // Replace our data dictionary with the new one, and say that we're
            // ready to poll again.
            data = newData;
            readyToPoll = true;
        }));

        StartCoroutine(Mongo.LoseHealth(HealthLostOnCycle));
        HealthLostOnCycle = 0;
    }

    public static void LoseHealth(float amount)
    {
        HealthLostOnCycle += amount;
    }
}
