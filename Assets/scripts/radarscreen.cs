using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarscreen : MonoBehaviour
{
    Vector3 force;                   // The vector to store the direction of the player's movement.
    public Rigidbody _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = GameObject.Find("light").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = Input.GetAxisRaw("Vertical");

        force.Set(-x, 0f, z);
        _light.AddForce(force);

    }
}
