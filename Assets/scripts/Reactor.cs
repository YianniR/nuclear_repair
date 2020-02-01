using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour {
    public float health = 10000f;

    private float untilNextTemperatureIncrease;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Debug.Log(health);
       

        float scaledlevel = 0.1f + 0.9f * health;
        this.transform.Find("health/bar").localScale = new Vector3(1, scaledlevel, 1);
    }
}
