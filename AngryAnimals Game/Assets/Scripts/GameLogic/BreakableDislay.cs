using UnityEngine;
using System.Collections;

public class BreakableDislay : MonoBehaviour {
	
	//Stores sprites for all destruction stages
	//of the breakable, from worst to best
	public Sprite[] destructionStages;
	
	//Reference to the breakable
	private Breakable brk;
	
	//Reference to the sprite renderer
	private SpriteRenderer sr;
	
	//Stores original health of the breakable
	private float fullHealth;
	
	// Use this for initialization
	void Start () {
		brk = GetComponent<Breakable>();
		fullHealth = brk.initialHealth;
		sr = GetComponent<SpriteRenderer>();
		UpdateDisplay (fullHealth);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Handles damage take message
	void DamageTaken(float newHealth){
		UpdateDisplay(newHealth);
	}
	
	//Updates displayed sprite based on health
	void UpdateDisplay(float health){
		//Change destruction stage based
		//on new health
		float healthRatio = health / fullHealth;
		int maxIndex = destructionStages.Length - 1;
		int matchingIndex = (int)(healthRatio * maxIndex);
		sr.sprite = destructionStages[matchingIndex];
	}
}
