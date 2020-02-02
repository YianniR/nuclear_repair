using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add the TextMesh Pro namespace to access the various functions.

public class numpad : Widget
{
    public bool isrunning = true;

    public string password;
    public string entered;

    float nextbreak = 0;
    public float now;
    public float breaktime;
    public float mintime = 2;
    public float maxtime = 10;

    TextMeshPro textmesh;
    Light light;

    public float impactWeight = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

        nextbreak = Time.time + Random.Range(mintime, maxtime);
        textmesh = GameObject.Find("screen/text").GetComponent<TextMeshPro>();
        light = this.transform.Find("light/LedLight").GetComponent<Light>();
        password = "";

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        textmesh.SetText(entered);
        now = Time.time;
        breaktime = nextbreak;

        if (isrunning)
        {
            textmesh.color = new Color(1, 1, 1);
            light.enabled = false;

            if (Time.time > nextbreak)
            {
                isrunning = false;
                password = Random.Range(0, 9999).ToString().PadLeft(4, '0');
            }
        }
        else //not running
        {
            light.enabled = true;
            WorldData.LoseHealth(impactWeight);
            textmesh.color = new Color(1, 0, 0);
            if(entered == password)
            {
                textmesh.color = new Color(0, 1, 0);
                isrunning = true;
                nextbreak = Time.time + Random.Range(mintime, maxtime);
            }

        }
    }

    public void press(int key)
    {
        if (entered.Length < 4)
        {
            string strkey = key.ToString();
            entered = entered + strkey;
        }
    }

    public void clear()
    {
        entered = "";
    }
    string randomint()
    {
        int PIN = Random.Range(0, 9);
        return PIN.ToString();
    }
}


