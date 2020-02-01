using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarscreen : MonoBehaviour
{
    Vector3 movement;                   // The vector to store the direction of the player's movement.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = Input.GetAxisRaw("Vertical");

        // Set the movement vector based on the axis input.
        movement.Set(x/10, 0f, z/10);
        // Move the player to it's current position plus the movement.
        this.transform.Find("light").transform.Translate(movement);
    }
}
