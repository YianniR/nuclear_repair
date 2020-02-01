using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reactor : MonoBehaviour {
    public float health = 100f;


    private float untilNextTemperatureIncrease;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Debug.Log(health);
        health = Mathf.Clamp(health, 0, 100f);
        this.transform.Find("health/Meter").localScale = new Vector3(1, 1, health/100f);
        if (health<=66f && health>=33f)
        {
            this.transform.Find("health/Meter/bar").GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
        if (health < 33f)
        {
            this.transform.Find("health/Meter/bar").GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

    public void lose () {
        Debug.Log("You LOSE!");
        // TODO Load the 'you lost' scene.
    }
}
