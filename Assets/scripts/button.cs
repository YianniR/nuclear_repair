using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class button : Widget {
    private Reactor reactor;

    public uint temperatureReduction = 1;
    public float depth = 0.1f;

    Transform cylinder;

    // Start is called before the first frame update
    void Start () {
        reactor = GameObject.FindGameObjectsWithTag("Reactor")[0].GetComponent<Reactor>();
        cylinder = transform.Find("Cylinder");
    }

    // Update is called once per frame
    void Update () { }


    public void OnMouseDown () {
        Debug.Log("OnMouseDown");
        cylinder.Translate(Vector3.down * depth);
    }

    public void OnMouseUp () {
        Debug.Log("OnMouseUp");
        cylinder.Translate(Vector3.up * depth);
        reactor.cool(temperatureReduction);
    }
}
