using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarscreen : MonoBehaviour
{
    Vector3 velocity;                   // The vector to store the direction of the player's movement.
    Vector3 randomvelocity;                   // The vector to store the direction of the player's movement.
    public GameObject _light;
    float nextbreak = 0;
    public float maxRadius;

    // Start is called before the first frame update
    void Start()
    {
        _light = transform.Find("radarlight").gameObject;
        nextbreak = Time.time + Random.Range(5, 10);

    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = Input.GetAxisRaw("Vertical");

        velocity.Set(x/10, 0f, z/10);
        _light.transform.localPosition += velocity;

        if (Time.time > nextbreak)
        {

            randomvelocity.Set(Random.Range(-0.01f, 0.01f), 0f, Random.Range(-0.01f, 0.01f));

            nextbreak = Time.time + Random.Range(5, 10);
        }

        _light.transform.localPosition += randomvelocity;

        Vector3 pos = new Vector3(_light.transform.localPosition.x, 0f, _light.transform.localPosition.z);
        pos = Vector3.ClampMagnitude(pos, maxRadius);
        _light.transform.localPosition = new Vector3(pos.x, _light.transform.localPosition.y, pos.z);
    }
}
