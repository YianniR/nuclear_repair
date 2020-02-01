using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablebtn : MonoBehaviour
{
    public char banned;
    TextMesh bantext;
    keyinputmanager inpmanager;

    // Start is called before the first frame update
    void Start()
    {
        bantext = GameObject.Find("letter/text").GetComponent<TextMesh>();
        inpmanager = GameObject.Find("/KeyInputManager").GetComponent<keyinputmanager>();
        newban();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        this.transform.Translate(new Vector3(0, -0.2f, 0));
        newban();
    }
    public void OnMouseUp()
    {
        this.transform.Translate(new Vector3(0, 0.2f, 0));
    }

    void newban()
    {
        inpmanager.setkeystate(banned, true);
        banned = randomchar();
        bantext.text = banned.ToString().ToUpper();
        inpmanager.setkeystate(banned, false);
    }

    char randomchar()
    {
        string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string mychar = Alphabet[Random.Range(0, 25)];
        return mychar.ToLower()[0];
    }
}
