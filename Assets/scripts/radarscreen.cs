using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarscreen : Widget
{
    Vector3 velocity;                   // The vector to store the direction of the player's movement.
    Vector3 randomvelocity;                   // The vector to store the direction of the player's movement.
    public GameObject _light;
    float nextbreak = 0;
    public float maxRadius;

    public float impactWeight = 0.01f;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        _light = transform.Find("screen/radarlight").gameObject;
        source = this.transform.GetComponent<AudioSource>();
        source.volume = 0;
        source.Play();
        nextbreak = Time.time + Random.Range(5, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = Input.GetAxisRaw("Vertical");

        velocity.Set(x/10, 0f, z/10);
        _light.transform.localPosition += velocity;

        if (Time.time > nextbreak)
        {

            randomvelocity.Set(Random.Range(-0.03f, 0.03f), 0f, Random.Range(-0.03f, 0.03f));

            nextbreak = Time.time + Random.Range(5, 10);
        }

        _light.transform.localPosition += randomvelocity;

        Vector3 pos = new Vector3(_light.transform.localPosition.x, 0f, _light.transform.localPosition.z);
        pos = Vector3.ClampMagnitude(pos, maxRadius);
        _light.transform.localPosition = new Vector3(pos.x, _light.transform.localPosition.y, pos.z);

        source.volume = pos.magnitude * 0.8f;

        if (pos.magnitude > maxRadius * 3f/4f)
        {
            _light.GetComponent<Light>().color = Color.red;
            _light.GetComponent<Light>().range = 0.31f; 
            WorldData.LoseHealth(impactWeight);
        }
        else
        {
            _light.GetComponent<Light>().color = Color.green;
            _light.GetComponent<Light>().range = 0.21f;
        }
    }
}
