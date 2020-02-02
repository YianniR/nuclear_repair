using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notepad : Widget {
    Director director;

    void Start() {
        director = GameObject.FindWithTag("Director").GetComponent<Director>();
    }

    void Update() { }

    void OnMouseUp () {
        Debug.Log("CLICKED " + Time.time.ToString());
        director.startSpawning();
    }
}
