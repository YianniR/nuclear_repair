using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    public float rate = 0.1f;
    public float finalInt = 0.0f;
    public float startInt = 10f;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Light>().intensity = startInt;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Light>().intensity > finalInt) {
            this.GetComponent<Light>().intensity -= rate;
        }
    }
}
