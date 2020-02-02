using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerIntensity : MonoBehaviour
{
    float change;
    public float minChange = -0.2f;
    public float maxChange = 0.2f;

    public float minInt = 0.85f;
    public float maxInt = 1.25f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        change = Random.Range(minChange, maxChange);
        this.GetComponent<Light>().intensity += change;
        this.GetComponent<Light>().intensity = Mathf.Clamp(this.GetComponent<Light>().intensity, minInt, maxInt);
    }
}
