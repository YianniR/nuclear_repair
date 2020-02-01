using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {
    private Reactor reactor;

    public int temperatureReduction = 1;

    // Start is called before the first frame update
    void Start () {
        reactor = GameObject.FindGameObjectsWithTag("Reactor")[0].GetComponent<Reactor>();
    }

    // Update is called once per frame
    void Update () { }

    [System.Serializable]
    public class data {
        public int numberOfClicks;
    }

    public void init (Vector3 pos, int id) {

    }

    public void send<T> (T t) {
        /* if (!T.IsSerializable) return; */

        string json = JsonUtility.ToJson (t);
    }

    public void OnMouseDown () {
        Debug.Log("OnMouseDown");
        reactor.temperature -= temperatureReduction;
    }

    public void OnMouseUp () {
        Debug.Log("OnMouseUp");
    }
}
