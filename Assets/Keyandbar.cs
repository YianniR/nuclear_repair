using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Keyandbar : MonoBehaviour
{
    public float downspeed = 1;
    public float upspeed = 1;
    string mychar;
    public float barlevel = 1;
    keyinputmanager inpmanager;
    // Start is called before the first frame update
    void Start()
    {
        string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        mychar = Alphabet[Random.Range(0, 25)];
        ((TextMesh)this.transform.Find("Text").GetComponent<TextMesh>()).text = mychar;
        inpmanager = GameObject.Find("KeyInputManager").GetComponent<keyinputmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        char realkey = inpmanager.MapKey(mychar.ToLower()[0]);

        if (inpmanager.KeyEn(mychar.ToLower()[0]))
        {
            if (Input.GetKeyDown(realkey.ToString()))
            {
                barup();
            }
            else
            {
                bardown();
            }
        }
        else
        {
            bardown();
        }
        float scaledlevel = 0.1f + 0.9f * barlevel;
        this.transform.Find("Meter").localScale = new Vector3(1, scaledlevel, 1);
    }


    void barup()
    {
        barlevel += upspeed;
        if (barlevel >= 1)
        {
            barlevel = 1;
        }
    }
    
    void bardown()
    {
        barlevel -= downspeed;
        if (barlevel <= 0)
        {
            barlevel = 0;
        }
    }


}
