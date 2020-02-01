using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour {
    public int id;
    public int temperature;
    public float secondsPerTemperatureIncrease = 1;


    private float untilNextTemperatureIncrease;

    // Start is called before the first frame update
    void Start() {
        untilNextTemperatureIncrease = secondsPerTemperatureIncrease;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(temperature);

        untilNextTemperatureIncrease -= Time.deltaTime;

        if (untilNextTemperatureIncrease <= 0) {
            temperature += 1;
            untilNextTemperatureIncrease = secondsPerTemperatureIncrease;
        }
    }
}
