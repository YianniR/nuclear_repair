using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyinputmanager : MonoBehaviour
{
    Dictionary<char, char> map = new Dictionary<char, char>();
    Dictionary<char, bool> en = new Dictionary<char, bool>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public char MapKey(char c)
    {
        if (!map.ContainsKey(c))
        {
            map.Add(c, c);
        }
        return map[c];
    }

    public bool KeyEn(char c)
    {
        if (!en.ContainsKey(c))
        {
            en.Add(c, true);
        }
        return en[c];
    }

    public void swapkeys(char a, char b)
    {
        if (!map.ContainsKey(a)){
            map.Add(a, a);
        }
        if (!map.ContainsKey(b))
        {
            map.Add(b, b);
        }

        char aval = map[a];
        char bval = map[b];
        map[a] = bval;
        map[b] = aval;
    }

    public void setkeystate(char c, bool e)
    {
        if (!en.ContainsKey(c))
        {
            en.Add(c, e);
        }
        else
        {
            en[c] = e;
        }
    }

}
