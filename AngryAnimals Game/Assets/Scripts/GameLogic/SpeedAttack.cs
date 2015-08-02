using UnityEngine;
using System.Collections;

public class SpeedAttack : MonoBehaviour {

    //Multiply the original speed by this value
    //when performing attack
    public float speedFactor = 1.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Performes speed attack
    public void DoSpecialAttack()
    {
        //Get the rigid body of the object
        Rigidbody2D myRB = GetComponent<Rigidbody2D>();

        //Multiply speed by the factor
        myRB.velocity = myRB.velocity * speedFactor;
    }
}
