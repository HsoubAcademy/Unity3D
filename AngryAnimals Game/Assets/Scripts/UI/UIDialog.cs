using UnityEngine;
using System.Collections;

public class UIDialog : MonoBehaviour {

    //Size of dialog when shown
    public Vector2 maxSize = Vector2.one;

    //Is dialog currently expanding?
    private bool expanding = false;

    //Is dialog currently shrinking
    private bool shrinking = false;

	// Use this for initialization
	void Start () {
        //Hidden by default
        transform.localScale = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
        //Get the local scale
        Vector2 scale = transform.localScale;

        //Compute the new scale
        if (expanding)
        {
            scale = Vector2.Lerp(scale, maxSize, Time.deltaTime * 10);
            //Check dead zone
            if (Vector2.Distance(scale, maxSize) < 0.01f)
            {
                scale = maxSize;
                expanding = false;
            }
        }
        else if (shrinking)
        {
            scale = Vector2.Lerp(scale, Vector2.zero, Time.deltaTime * 10);
            //Check dead zone
            if (Vector2.Distance(scale, Vector2.zero) < 0.01f)
            {
                scale = Vector2.zero;
                shrinking = false;
            }
        }

        //Set the new scale
        transform.localScale = scale;
	}

    //Show dialog
    public void Show()
    {
        expanding = true;
        shrinking = false;
    }

    //Hide dialog
    public void Hide()
    {
        shrinking = true;
        expanding = false;
    }
}
