using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarscreen : MonoBehaviour
{
    Vector3 force;                   // The vector to store the direction of the player's movement.
    Vector3 randomForce;                   // The vector to store the direction of the player's movement.
    public Rigidbody _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = transform.Find("radarlight").gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = Input.GetAxisRaw("Vertical");

        randomForce.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
        force.Set(-x, 0f, z);
        _light.AddForce(force);
        _light.AddForce(randomForce);

        //_light.position = Vector3.ClampMagnitude(_light.position, 2.5f);

    }
}
