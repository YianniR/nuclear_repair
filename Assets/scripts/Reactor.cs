using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reactor : Widget
{
    public float health = 100f;
    public float maxHealth = 100f;
    public GameObject meter;
    public GameObject bar;

    private bool _readyToPoll = true;
    public float SecondsPerPoll = 0.5f;
    public float PollTimeoutDeadline = 1.5f;
    private float _untilNextPoll;
    private float _untilPollTimeoutDeadline;

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
        _untilPollTimeoutDeadline -= Time.deltaTime;
        if (!_readyToPoll && _untilPollTimeoutDeadline <= 0.0f)
        {
            _readyToPoll = true;
        }

        // Reduce time to poll
        _untilNextPoll -= Time.deltaTime;

        if (_untilNextPoll <= 0.0f && _readyToPoll)
        {
            StartCoroutine(Mongo.GetHealthLost(UpdateHealth));
            _untilNextPoll = SecondsPerPoll;
        }
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