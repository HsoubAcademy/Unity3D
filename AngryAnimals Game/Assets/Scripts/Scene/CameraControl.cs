using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	//Minimum size of the camera when zooming in
	public float minSize = 2.5f;
	
	//The height of camera view rectangle
	private float camHeight;
	
	//The width of camera view rectangle
	private float camWidth;
	
	//Reference to camera component
	Camera cam;
	
	//Stores current scene bounds
	private Bounds sceneBounds;
		
	// Use this for initialization
	void Start () {
		//Get component on the same object
		cam = GetComponent<Camera>();
		//Update the dimensions
		UpdateCamDimensions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//Hhandle change in scene bounds
	void SceneBoundsChanged(Bounds newBounds){
		sceneBounds = newBounds;
	}
	
	//Moves the camera by specific distance
	public void Move(Vector2 distance){
		//Compute allowed limits
		float maxX = GetRightLimit();
		float minX = GetLeftLimit();
		float maxY = GetUpperLimit();
		float minY = GetLowerLimit();

        //In this special case, 3D position is
        //important to make sure that camera is
        //always at z = -10
        Vector3 dist3D = distance;

		//Compute new positin
        Vector3 newPos = transform.position + dist3D;
		
		//Enfore limits
		newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        newPos.z = -10.0f;
		//Change position
		transform.position = newPos;
	}
	
	//Changes camera zoom:
	//positive = zoom in
	//negative = zoom out
	public void Zoom(float amount){
		float newSize = cam.orthographicSize - amount;
		
		//Enforce limits
		float maxSize = GetMaxSize();
		newSize = Mathf.Clamp (newSize, minSize, maxSize);
		
		//Change camera size (and eventually zoom)
		cam.orthographicSize = newSize;
		
		//Move zero units to enforce valid position
		UpdateCamDimensions();
        Move(Vector2.zero);
	}
	
	//Updates height and width
	void UpdateCamDimensions(){
		camHeight = cam.orthographicSize * 2;
		camWidth = camHeight * cam.aspect;
	}
	
	//Max y position for the camera
	float GetUpperLimit(){
		return sceneBounds.max.y - camHeight * 0.5f;
	}
	
	//Min y position for the camera
	float GetLowerLimit(){
		return sceneBounds.min.y + camHeight * 0.5f;
	}
	
	//Max x position for the camera
	float GetRightLimit(){
		return sceneBounds.max.x - camWidth * 0.5f;
	}
	
	//Min x position for the camera
	float GetLeftLimit(){
		return sceneBounds.min.x + camWidth * 0.5f;
	}
	
	//Computes maximum allowed size of the camera
	//based on scene bounds
	float GetMaxSize(){
		float maxHeight = sceneBounds.max.y - sceneBounds.min.y;
		return maxHeight * 0.5f;
	}
}
