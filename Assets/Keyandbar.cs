using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Keyandbar : MonoBehaviour
{
    public float downspeed = 1;
    public float upspeed = 1;
    string mychar;
    public float barlevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        mychar = Alphabet[Random.Range(0, 25)];
        ((TextMesh)this.transform.Find("Text").GetComponent<TextMesh>()).text = mychar;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(mychar.ToLower())){
            barlevel += upspeed;
            if (barlevel >= 1)
            {
                barlevel = 1;
            }
        }
        else
        {
            barlevel -= downspeed;
            if (barlevel <= 0)
            {
                barlevel = 0;
            }
        }

        float scaledlevel = 0.1f + 0.9f * barlevel;
        this.transform.Find("Meter").localScale = new Vector3(1, scaledlevel, 1);
    }


}
