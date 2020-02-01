using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyswapper : MonoBehaviour
{
    public char key1;
    public char key2;
    bool haveirun = false;
    float next_swap;
    GameObject txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = this.transform.Find("Text/tex").gameObject;
        swapkeys();
        next_swap = Time.time + Random.Range(5f, 25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > next_swap)
        {
            swapkeys();
            next_swap = Time.time + Random.Range(5f, 25f);
        }
    }

    char randomchar()
    {
        string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string mychar = Alphabet[Random.Range(0, 25)];
        return mychar.ToLower()[0];
    }

    void swapkeys()
    {
        keyinputmanager swp = (keyinputmanager)GameObject.Find("/KeyInputManager").GetComponent<keyinputmanager>();

        if (haveirun){
            swp.swapkeys(key1, key2);
        }
        else
        {
            haveirun = true;
        }
        
        key1 = randomchar();
        key2 = randomchar();
        swp.swapkeys(key1, key2);

        txt.GetComponent<TextMesh>().text = key1.ToString().ToUpper() + "⇄" + key2.ToString().ToUpper();

    }
}