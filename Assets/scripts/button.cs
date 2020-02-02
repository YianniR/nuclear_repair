using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class button : Widget
{
    public bool isrunning = true;
    public uint temperatureReduction = 1;
    public float depth = 0.1f;
    public float now;
    public float breaktime;
    float nextbreak = 0;
    bool pressed = false;
    Transform cylinder;
    public float mintime = 2;
    public float maxtime = 10;

    public float impactWeight = 0.01f;

    Light light;

    // Start is called before the first frame update
    void Start()
    {
        cylinder = this.transform.Find("Cylinder");
        light = this.transform.Find("light/LedLight").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        now = Time.time;
        if (isrunning)
        {
            light.enabled = false;

            if (Time.time > nextbreak)
            {
                isrunning = false;
            }
        }
        else //not running
        {
            light.enabled = true;
            WorldData.LoseHealth(impactWeight);

            if (pressed)
            {
                isrunning = true;
                nextbreak = Time.time + Random.Range(mintime, maxtime);
            }
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        cylinder.Translate(Vector3.down * depth);
        pressed = true;
    }

    public void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        cylinder.Translate(Vector3.up * depth);
        pressed = false;
    }
}