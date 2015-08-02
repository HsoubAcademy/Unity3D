using UnityEngine;
using System.Collections;

public class BreakableSounds : MonoBehaviour {

    //Sound to play when damage is taken
    public AudioClip damageSound;

    //Sound to play upon destruction
    public AudioClip destructionSound;

    //Minimum damage amount to play damage sound
    public float minDamage = 25.0f;

    //How long to suspend playing clips
    private float suspensionTime = 0.0f;

    //When was last clip played
    private float playTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Receives DamageTaken message
    void DamageTaken(float amount)
    {
        //Only considerable damage should
        //reult in sound
        if (amount >= minDamage)
        {
            CheckAndPlay(damageSound);
        }
    }

    //Receives BreakableDestroyed message
    void BreakableDestroyed()
    {
        AudioSource.PlayClipAtPoint(destructionSound, transform.position);
    }

    //Checks suspension time and plays clip if possible
    void CheckAndPlay(AudioClip clip)
    {
        if (Time.time - playTime > suspensionTime)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, 0.25f);
            playTime = Time.time;
            suspensionTime = clip.length + 0.5f;
        }
    }
}
