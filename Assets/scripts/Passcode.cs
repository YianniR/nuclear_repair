using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passcode : Widget
{
    [System.Serializable]
    private struct Code {
        string code;

        public Code (string c) { this.code = c; }
    }

    private WorldData world;
    private TextMesh text;

    private int numDigits = 4;

    void Start() {
        world = GameObject.FindWithTag("WorldData").GetComponent<WorldData>();
        text = this.transform.Find("Text/tex").gameObject.GetComponent<TextMesh>();

        string code = makeCode();

        world.set<Code>(new Code(code), dataIds[0]);
        text.text = code;
    }

    void Update() { }

    private string makeCode () {
        string[] numerals = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string digits = "";

        for (int i=0; i<numDigits; i++) {
            digits += numerals[Random.Range(0, numerals.Length)];
        }

        return digits;
    }
}
