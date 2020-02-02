using UnityEngine;
using UnityEngine.UI;  // add to the top
using System.Collections;

public class fadeout : MonoBehaviour
{

    CanvasGroup myCG; 
    private bool flash = false;

    // Start is called before the first frame update
    void Start()
    {
        myCG = this.transform.GetComponent<CanvasGroup>();
        flash = true;
        myCG.alpha = 1;
    }

    void FixedUpdate()
    {
        if (flash)
        {
            myCG.alpha = myCG.alpha - Time.deltaTime;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }
}