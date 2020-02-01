using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reactor : MonoBehaviour {
    public float health = 100f;
    public GameObject meter;


    private float untilNextTemperatureIncrease;

    // Start is called before the first frame update
    void Start() {
        meter = this.transform.Find("health/Meter").gameObject;
    }

    // Update is called once per frame
    void Update() {
        health = Mathf.Clamp(health, 0, 100f);
        meter.transform.localScale = new Vector3(1, 1, health/100f);
    }

    public void lose () {
        Debug.Log("You LOSE!");
        // TODO Load the 'you lost' scene.
    }
}
