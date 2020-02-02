using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearsmallbutton : MonoBehaviour
{
    public numpad prnt;
    // Start is called before the first frame update
    void Start()
    {
        prnt = this.transform.parent.gameObject.GetComponent<numpad>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        prnt.clear();
    }
}
