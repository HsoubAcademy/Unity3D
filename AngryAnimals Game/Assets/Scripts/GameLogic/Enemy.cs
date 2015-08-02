using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Receives BreakableDestroyed message and reports
    //enemy destruction to the scene root
    void BreakableDestroyed()
    {
        SendMessageUpwards("EnemyDestroyed");
    }
}
