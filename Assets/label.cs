using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class label : MonoBehaviour
{
    public string label_name1;
    public string label_name2;
    public string label_name3;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Find("Keyandbar/Label1").GetComponent<TextMesh>().text = label_name1;
        this.transform.Find("Keyandbar (1)/Label2").GetComponent<TextMesh>().text = label_name2;
        this.transform.Find("Keyandbar (2)/Label3").GetComponent<TextMesh>().text = label_name3;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
