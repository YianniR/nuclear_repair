using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyinputmanager : MonoBehaviour
{
    Dictionary<char, char> map = new Dictionary<char, char>();
    Dictionary<char, int> en = new Dictionary<char, int>();
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
            en.Add(c, 0);
        }
        if (en[c] == 0){
            return true;
        }
        else
        {
            return false;
        }
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
            if (e)
            {
                en.Add(c, 0);
            }
            else
            {
                en.Add(c, 1);
            }
        }
        else
        {
            if (e) { en[c] -= 1; }
            else { en[c] += 1; }
        }
    }

}
