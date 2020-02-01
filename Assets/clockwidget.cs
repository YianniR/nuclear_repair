using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockwidget : MonoBehaviour
{
    float endtime;
    public int numsecs = 300;
    public int grace = 5;
    TextMesh minstxt;
    TextMesh secstxt;
    // Start is called before the first frame update
    void Start()
    {
        endtime = Time.time + numsecs + grace;
        minstxt = GameObject.Find("Text/mins").GetComponent<TextMesh>();
        secstxt = GameObject.Find("Text/secs").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeleft = endtime - Time.time;

        if (timeleft > numsecs)
        {
            timeleft = numsecs;
        }
        else if (timeleft < 0)
        {
            timeleft = 0;
        }
        int mins = ((int)timeleft) / 60;
        int secs = (int)timeleft - 60 * mins;

        minstxt.text = mins.ToString().PadLeft(2, '0');
        secstxt.text = secs.ToString().PadLeft(2, '0');
        
    }
}
