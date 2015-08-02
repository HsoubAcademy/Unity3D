using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {
	
	//Initial health of the breakable
	public float initialHealth = 100;
	
	//Internal health storage
	private float health;
	
	// Use this for initialization
	void Start () {
		health = initialHealth;
	}

	// Update is called once per frame
	void Update () {
	
	}
	
	//Handles damage
	public void TakeDamage(float amount){
        //No more damage for destroyed objects
        if (health <= 0.0f)
        {
            return;
        }

		if(health <= amount){
			//Breakable destroyed
            health = 0.0f;
			SendMessageUpwards("BreakableDestroyed");
			Destroy(gameObject);
			return;
		}
		
		//Reduce health by damage amount
		health -= amount;
		
		//Inform about change in health
		SendMessage("DamageTaken", health);
	}
}
