using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    //Seconds to live after launching
    public float lifeSpan = 7.5f;

    //Can the player control this projectile?
    private bool controllable = false;

    //Is this projectile held ready for launching?
    private bool held = false;

    //Has this projectile already been launched?
    private bool launched = false;

    //Whether special attack has been performed
    private bool attackPerformed = false;

    //Stores the position where the projectile has been held
    private Vector2 holdPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Makes the projectile controllable
    public void AllowControl()
    {
        if (!launched)
        {
            controllable = true;
        }
    }

    //Hold this projectile to make it ready for launching
    public void Hold()
    {
        if (controllable && !held && !launched)
        {
            held = true;
            holdPosition = transform.position;
            //Send hold message
            SendMessage("ProjectileHeld");
        }
    }

    //Launch this projectile using the specified launch force
    public void Launch(float forceMultiplier)
    {
        if (controllable && held && !launched)
        {
            //Compute launch vector
            Vector2 launchPos = transform.position;
            Vector2 launchForce = (holdPosition - launchPos) * forceMultiplier;
            //Add launch force
            Rigidbody2D myRB = GetComponent<Rigidbody2D>();
            myRB.isKinematic = false;
            myRB.AddForce(launchForce, ForceMode2D.Impulse);
            //Set flags
            launched = true;
            held = false;
            controllable = false;
            //Destroy after a while
            Destroy(gameObject, lifeSpan);
            //Send launch message
            SendMessage("ProjectileLaunched");
        }
    }

    //Calls for special attack that is performed after launching
    public void PerformSpecialAttack()
    {
        if (!attackPerformed && launched)
        {
            //Allowed only one time
            attackPerformed = true;
            SendMessage("DoSpecialAttack", SendMessageOptions.DontRequireReceiver);
        }
    }

    //Drags the projectile to the specified position
    public void Drag(Vector2 position)
    {
        if (controllable && held && !launched)
        {
            transform.position = position;
        }
    }

    //Tells whether the projectile is currently held
    public bool IsHeld()
    {
        return held;
    }

    //Tells whether the projectile is launched
    public bool IsLaunched()
    {
        return launched;
    }
}
