using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	
	//Prefab to generate elements from
	public GameObject bgElementPrefab;
	
	//Array of all available backgrounds
	public Sprite[] selectionList;

	//How many times shoud BG image repeated
	public int repeatCount = 4;

	//Currently displayed background
	private int selectedIndex = -1;

	//Reference to the child "Background" empty object
	private Transform bgGameObject;

	// Use this for initialization
	void Start () {
		//Find the child object called "Background"
		bgGameObject = transform.FindChild("Background");
		//Set the default background
		ChangeBackground(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Changes currently displayed background
	public void ChangeBackground(int newIndex){
		if(newIndex == selectedIndex){
			//No need to change
			return;
		}

		//Delete current background sprites
		for(int i = 0; i < bgGameObject.childCount; i++){
			Transform bgSprite = bgGameObject.GetChild(i);
			Destroy(bgSprite.gameObject);
		}

		//Add new background sprites
		Sprite newSprite = selectionList[newIndex];

		//What is the width of each sprite (in pixels)?
		float width = newSprite.rect.width;
		
		//What is the height of each sprite (in pixels)?
		float height = newSprite.rect.height;

		//How many pixels per unit?
		float ppu = newSprite.pixelsPerUnit;

		//The actual width and height in units
		width = width / ppu;
		height = height / ppu;

		//What is the left most (start) position?
		float posX = -width * repeatCount * 0.5f;
		
		//New bounds of the scene
		Vector2 boundsSize = new Vector2(width * repeatCount, height);
		Bounds newBounds = new Bounds(Vector2.zero, boundsSize);
		
		//Inform about new change in scene bounds
		BroadcastMessage("SceneBoundsChanged", newBounds);

		//Now we can start creating the background
		for(int i = 0; i < repeatCount; i++){
			//Create new object from the prefab
			GameObject bg = (GameObject) Instantiate(bgElementPrefab);
			//Set the name
			bg.name = "BG_" + i;
			//Get the sprite renderer component
			SpriteRenderer sr = bg.GetComponent<SpriteRenderer>();
			//Set the sprite to the selected one
            sr.sprite = newSprite;
			//Set the position to the one we already computed
			bg.transform.position = new Vector2(posX, 0);
            //Put it to the back
            sr.sortingOrder = -1;
			//Add it as a child to "Background" empty object
			bg.transform.parent = bgGameObject;
			//Add box collider
			bg.AddComponent<BoxCollider2D>();
			//Increment the x position for the next sprite
			posX += width;
		}
		
		//Inform about background change
		BroadcastMessage("BackgroundChanged", newIndex);
	}
}
