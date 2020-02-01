using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add the TextMesh Pro namespace to access the various functions.

public class numpad : MonoBehaviour
{
    public bool isrunning = true;

    string to_enter;
    public string entered;

    float nextbreak = 0;
    public float now;
    public float breaktime;
    public float mintime = 2;
    public float maxtime = 10;

    small_button _0;
    small_button _1;
    small_button _2;
    small_button _3;
    small_button _4;
    small_button _5;
    small_button _6;
    small_button _7;
    small_button _8;
    small_button _9;
    TextMeshPro textmesh;

    // Start is called before the first frame update
    void Start()
    {
        nextbreak = Time.time + Random.Range(mintime, maxtime);
        _0 = GameObject.Find("0").GetComponent<small_button>();
        _1 = GameObject.Find("1").GetComponent<small_button>();
        _2 = GameObject.Find("2").GetComponent<small_button>();
        _3 = GameObject.Find("3").GetComponent<small_button>();
        _4 = GameObject.Find("4").GetComponent<small_button>();
        _5 = GameObject.Find("5").GetComponent<small_button>();
        _6 = GameObject.Find("6").GetComponent<small_button>();
        _7 = GameObject.Find("7").GetComponent<small_button>();
        _8 = GameObject.Find("8").GetComponent<small_button>();
        _9 = GameObject.Find("9").GetComponent<small_button>();
        textmesh = GameObject.Find("screen/text").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        textmesh.SetText(entered);

        now = Time.time;
        breaktime = nextbreak;

        if (isrunning)
        {
            //Light light = this.transform.Find("screen/light").GetComponent<Light>();
            //light.enabled = false;

            if (Time.time > nextbreak)
            {
                isrunning = false;
            }
        }
        else //not running
        {
            //Light light = this.transform.Find("screen/light").GetComponent<Light>();
            //light.enabled = true;
          
            if (_0.pressed) { entered += "0"; }
            if (_1.pressed) { entered += "1"; }
            if (_2.pressed) { entered += "2"; }
            if (_3.pressed) { entered += "3"; }
            if (_4.pressed) { entered += "4"; }
            if (_5.pressed) { entered += "5"; }
            if (_6.pressed) { entered += "6"; }
            if (_7.pressed) { entered += "7"; }
            if (_8.pressed) { entered += "8"; }
            if (_9.pressed) { entered += "9"; }

            if(entered.Length > 4)
            {
                entered = entered.Substring(1, 4);
            }

            if(entered == to_enter)
            {
                isrunning = true;
                nextbreak = Time.time + Random.Range(mintime, maxtime);
            }

        }
    }

    string randomint()
    {
        int PIN = Random.Range(0, 9);
        return PIN.ToString();
    }
}


