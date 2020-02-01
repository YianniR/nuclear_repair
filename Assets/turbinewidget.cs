using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turbinewidget : MonoBehaviour
{
    public bool isrunning = true;
    string to_enter = "";
    public float rotspeed = 0;
    float smooth_rotspeed = 5;
    float smooth_down = 0.07f;
    float smooth_up = 0.01f;
    float nextbreak = 0;
    public float now;
    public float breaktime;
    public float mintime = 2;
    public float maxtime = 10;

    public GameObject reactor;
    public float impactWeight = 0.01f;

    GameObject turbine;
    TextMesh codetext;
    Light light;
    keyinputmanager inpmanager;

    // Start is called before the first frame update
    void Start()
    {
        reactor = GameObject.FindWithTag("Reactor");
        nextbreak = Time.time + Random.Range(mintime, maxtime);
        turbine = this.transform.Find("turbine").gameObject;
        codetext = this.transform.Find("Text/tex").GetComponent<TextMesh>();
        light = this.transform.Find("light/LedLight").GetComponent<Light>();
        inpmanager = GameObject.Find("KeyInputManager").GetComponent<keyinputmanager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        turbine.transform.Rotate(new Vector3(0, 1, 0), smooth_rotspeed);
        codetext.text = to_enter;

        now = Time.time;
        breaktime = nextbreak;
        if (isrunning)
        {
            light.enabled = false;
            to_enter = "";
            smooth_rotspeed += smooth_up;
            if (smooth_rotspeed > rotspeed)
            {
                smooth_rotspeed = rotspeed;
            }

            if(Time.time > nextbreak)
            {
                isrunning = false;
                to_enter = randomchar() + randomchar() + randomchar() + randomchar();
            }
        }
        else //not running
        {
            light.enabled = true;
            reactor.GetComponent<Reactor>().health -= impactWeight;
            smooth_rotspeed -= smooth_down;
            if (smooth_rotspeed < 0)
            {
                smooth_rotspeed = 0;
            }
            if (to_enter.Length > 0)
            {
                char realkey = inpmanager.MapKey(to_enter.ToLower()[0]);

                if (inpmanager.KeyEn(to_enter.ToLower()[0]))
                {
                    if (Input.GetKeyDown(realkey.ToString()))
                    {
                        to_enter = to_enter.Substring(1);
                    }
                }
            }
            else
            {
                isrunning = true;
                nextbreak = Time.time + Random.Range(mintime, maxtime);
            }

        }
    }

    string randomchar()
    {
        string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string mychar = Alphabet[Random.Range(0, 25)];
        return mychar;
    }
}


