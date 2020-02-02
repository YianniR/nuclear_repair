using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reactor : Widget
{
    public float health = 100f;
    public float maxHealth = 100f;
    public GameObject meter;
    public GameObject bar;


    private float _untilNextTemperatureIncrease;

    // Start is called before the first frame update
    void Start()
    {
        meter = this.transform.Find("health/Meter").gameObject;
        bar = this.transform.Find("health/Meter/bar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        meter.transform.localScale = new Vector3(1, 1, health / 100f);
        if (health <= 66f && health >= 33f)
        {
            bar.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }

        if (health < 33f)
        {
            bar.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

    void FixedUpdate()
    {
        StartCoroutine(Mongo.GetHealthLost(UpdateHealth));
    }

    void UpdateHealth(float lost)
    {
        var newHealth = maxHealth - lost;
        if (newHealth <= health)
        {
            health = newHealth;
        }
    }

    public void lose()
    {
        Debug.Log("You LOSE!");
        // TODO Load the 'you lost' scene.
    }
}