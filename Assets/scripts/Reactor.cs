using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reactor : MonoBehaviour {
    public int id;
    public int temperature;
    public float secondsPerTemperatureIncrease = 1;

    public float maxTemperature = 10;

    private float untilNextTemperatureIncrease;

    // Start is called before the first frame update
    void Start() {
        untilNextTemperatureIncrease = secondsPerTemperatureIncrease;
    }

    // Update is called once per frame
    void Update() {
        // Temperature increase
        untilNextTemperatureIncrease -= Time.deltaTime;
        if (untilNextTemperatureIncrease <= 0) {
            temperature += 1;
            untilNextTemperatureIncrease = secondsPerTemperatureIncrease;
            if (temperature > maxTemperature)
                lose();
        }
    }

    public void cool (uint amt) {
        temperature = System.Math.Max(0, temperature - (int)amt);
    }

    public void lose () {
        Debug.Log("You LOSE!");
        // TODO Load the 'you lost' scene.
    }
}
