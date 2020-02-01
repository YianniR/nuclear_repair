using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class widgetcamzoom : MonoBehaviour
{
    public int num_wig = 1;
    Camera cam;
    public Vector3 tgtpos;
    public float tgtsize;

    Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();

        cam = this.transform.GetComponent<Camera>();
        updatetgts();
        this.transform.position = tgtpos;
        cam.orthographicSize = tgtsize;
    }

    // Update is called once per frame
    void Update()
    {
        updatetgts();

        Vector3 oldpos = this.transform.position;
        this.transform.position = oldpos * 0.95f + tgtpos * 0.05f;

        float oldsize = cam.orthographicSize;

        cam.orthographicSize = oldsize * 0.95f + tgtsize * 0.05f;
    }

    int getnumwig()
    {
        return spawner.numWidgets();
    }

    void updatetgts()
    {
        int oldwig = num_wig;
        num_wig = getnumwig();
        if (num_wig > oldwig)
        {
            int l = 1;
            while (l * l < num_wig)
            {
                l += 1;
            }
            if (l % 2 == 0)
            {
                tgtpos = new Vector3(3, 20, 3);
            }
            else
            {
                tgtpos = new Vector3(0, 20, 0);
            }

            tgtsize = 3 * l;
        }
    }
}