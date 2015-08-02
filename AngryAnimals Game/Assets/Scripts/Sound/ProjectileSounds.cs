using UnityEngine;
using System.Collections;

public class ProjectileSounds : MonoBehaviour {

    //Played when the projectile is launched
    public AudioClip launchSound;

    //Played when the projectile is held
    public AudioClip holdSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void ProjectileHeld()
    {
        AudioSource.PlayClipAtPoint(holdSound, transform.position);
    }

    void ProjectileLaunched()
    {
        AudioSource.PlayClipAtPoint(launchSound, transform.position);
    }
}
