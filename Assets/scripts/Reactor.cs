using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reactor : Widget
{
    public float maxHealth = 100f;
    private float health;
    public GameObject meter;
    public GameObject bar;

    public AudioClip audio1;
    public AudioClip audio2;
    public AudioClip audio3;
    AudioSource source;

    bool audio2Playing = false;
    bool audio3Playing = false;

    private bool _readyToPoll = true;
    public float PollTimeoutDeadline = 1.5f;
    private float _untilNextPoll;
    private float _untilPollTimeoutDeadline;

    private float _untilNextTemperatureIncrease;

    void Awake()
    {
        health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        meter = this.transform.Find("health/Meter").gameObject;
        bar = this.transform.Find("health/Meter/bar").gameObject;
        source = this.transform.GetComponent<AudioSource>();
        source.clip = audio1;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        meter.transform.localScale = new Vector3(1, 1, health / maxHealth);

        if (health <= 66f && health >= 33f)
        {
            if (!audio2Playing)
            {
                source.clip = audio2;
                source.Play();
                audio2Playing = true;
            }
 
            bar.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }

        if (health < 33f)
        {
            if (!audio3Playing)
            {
                source.clip = audio3;
                source.Play();
                audio3Playing = true;
            }

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
            _untilNextPoll = WorldData.secondsPerPoll;
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