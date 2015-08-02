using UnityEngine;
using System.Collections;

public class BreakableSpawn : MonoBehaviour {

    //Prefab to instantiate
    public GameObject prefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Receive BreakableDestroyed message
    void BreakableDestroyed()
    {
        //Instantiate the desired object on its position
        GameObject newObject = (GameObject)Instantiate(prefab);
        newObject.transform.position = transform.position;
        newObject.transform.parent = transform.parent;
    }
}
