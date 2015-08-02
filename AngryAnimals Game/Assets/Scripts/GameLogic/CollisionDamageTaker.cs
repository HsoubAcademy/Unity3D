using UnityEngine;
using System.Collections;

public class CollisionDamageTaker : MonoBehaviour {

    //When did the last collision happen
    //This prevents consecutive damages
    private float lastCollisionTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Called upon collision with another object
    void OnCollisionEnter2D(Collision2D col)
    {
        if (Time.time - lastCollisionTime < 0.25f)
        {
            //Very short time since last damage
            return;
        }

        //Record colision time
        lastCollisionTime = Time.time;

        //Get the transform of the other object
        Transform otherObject = col.transform;

        //Get the heights of the objects
        float myHeight = transform.position.y;
        float otherHeight = otherObject.position.y;

        //How much damage should I take
        float damageAmount = 1.0f;

        //Determine damage based on the position of
        //the other object
        if (myHeight < otherHeight)
        {
            //Something has fallen on me, 

            //How fast was the fallen object?
            float fallVelocity = col.relativeVelocity.magnitude;
            //Multiply factor by velocity
            damageAmount *= fallVelocity;

            //How heavy was the fallen object?
            Rigidbody2D otherRB = otherObject.GetComponent<Rigidbody2D>();
            float otherMass = otherRB.mass;
            //Multiply factor again by mass
            damageAmount *= otherMass;
            //Finally duplicate the damage
            damageAmount *= 2.0f;
        }
        else
        {
            //I've fallen, damage is normal
            //Faster falling objects get higher damage
            float myFallVelocity = col.relativeVelocity.magnitude;
            //Multiply damage by velocity
            damageAmount *= myFallVelocity;
        }
        
        //Send damage taking message
        SendMessage("TakeDamage", damageAmount);
    }
}
