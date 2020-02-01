using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class small_button : MonoBehaviour
{
    public bool pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        this.pressed = true;
    }

    public void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        this.pressed = false;
    }
}
