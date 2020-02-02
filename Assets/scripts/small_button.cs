using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class small_button : MonoBehaviour
{
    public bool pressed = false;
    public numpad numpadprnt;
    public int keyval;
    // Start is called before the first frame update
    void Start()
    {
        numpadprnt = this.transform.parent.gameObject.GetComponent<numpad>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseDown()
    {
        numpadprnt.press(keyval);
    }
}

