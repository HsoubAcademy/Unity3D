using UnityEngine;
using System.Collections;

public class GroundManager : MonoBehaviour {
	
	//Ground element prefab
	public GameObject gePrefab;
	
	//A list of ground sprites to select from
	public Sprite[] selectionList;
	
	//Stores current bounds
	private Bounds sceneBounds;
	
	//Index of the selected ground
	private float selectedIndex = -1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Handle background change message
	void BackgroundChanged(int newIndex){
		ChangeGround(newIndex);
	}
	
	//Handle bounds change message
	void SceneBoundsChanged(Bounds newBounds){
		sceneBounds = newBounds;
	}
		
	//Change ground to selected index
	public void ChangeGround(int newIndex){
		//Reference to the child "Ground" empty object
		Transform groundGameObject = transform.FindChild("Ground");
		
		//Delete current ground elements
		for(int i = 0; i < groundGameObject.childCount; i++){
			Transform ge = groundGameObject.GetChild(i);
			Destroy(ge.gameObject);
		}
		
		//Compute scene width
		float sceneWidth = sceneBounds.max.x - sceneBounds.min.x;
		
		//Compute left most point
		float posX = sceneBounds.min.x;
		
		//Get ground sprite
		Sprite newGround = selectionList[newIndex];
		
		//Get sprite width and height
		float width = newGround.rect.width;
		float height = newGround.rect.height;
		
		//compute width and height in units
		width = width / newGround.pixelsPerUnit;
		height = height / newGround.pixelsPerUnit;
		
		//Compute y position of the ground
		float posY = sceneBounds.min.y + height * 0.5f;
		
		//How many times we have to repeat?
		int repeats = Mathf.RoundToInt(sceneWidth / width) + 1;
		
		for(int i = 0; i < repeats; i++){
			//Add new ground element
			GameObject ge = (GameObject) Instantiate(gePrefab);
			ge.name = "GE_" + i;
			
			//Set the position
            ge.transform.position = new Vector2(posX, posY);
			
			//Set parent
			ge.transform.parent = groundGameObject;
			
			//Set the sprite
			SpriteRenderer sr = ge.GetComponent<SpriteRenderer>();
			sr.sprite = newGround;
			
			//Add collider
			ge.AddComponent<BoxCollider2D>();
			
			//Increment x position
			posX += width;
		}
	}
}
