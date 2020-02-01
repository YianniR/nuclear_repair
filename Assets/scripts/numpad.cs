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

    // Start is called before the first frame update
    void Start()
    {
        nextbreak = Time.time + Random.Range(mintime, maxtime);

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Find("screen/text").GetComponent<TextMeshPro>().SetText(entered);

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
          
            if (this.transform.Find("0").GetComponent<small_button>().pressed) { entered += "0"; }
            if (this.transform.Find("1").GetComponent<small_button>().pressed) { entered += "1"; }
            if (this.transform.Find("2").GetComponent<small_button>().pressed) { entered += "2"; }
            if (this.transform.Find("3").GetComponent<small_button>().pressed) { entered += "3"; }
            if (this.transform.Find("4").GetComponent<small_button>().pressed) { entered += "4"; }
            if (this.transform.Find("5").GetComponent<small_button>().pressed) { entered += "5"; }
            if (this.transform.Find("6").GetComponent<small_button>().pressed) { entered += "6"; }
            if (this.transform.Find("7").GetComponent<small_button>().pressed) { entered += "7"; }
            if (this.transform.Find("8").GetComponent<small_button>().pressed) { entered += "8"; }
            if (this.transform.Find("0").GetComponent<small_button>().pressed) { entered += "9"; }

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


