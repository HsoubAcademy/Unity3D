using UnityEngine;
using System.Collections;

public class PathDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Receives level load message
    //and eventually removes the path
    //from the last leve (if any)
    void NewLevelLoaded()
    {
        GameObject path = GameObject.Find("Path");
        for (int i = 0; i < path.transform.childCount; i++)
        {
            Destroy(path.transform.GetChild(i).gameObject);
        }
    }
}
