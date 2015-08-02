using UnityEngine;
using System.Collections;

public class CameraMouseInput : MonoBehaviour {
	
	//Speed of camera movement
	public float movementSpeed = 0.1f;
	
	//Speed of zoom
	public float zoomingSpeed = 0.01f;
	
	//True when zooming
	private bool zoomingAcitve = false;
	
	//Stores the position of mouse from last frame
    private Vector2 lastPosition;
	
	//Reference to camera control script
	private CameraControl camControl;
	
	// Use this for initialization
	void Start () {
		//Find camera controller
		camControl = FindObjectOfType<CameraControl>();
	}
	
	//Called later than Update
	void LateUpdate () {
		UpdateZooming();
	}
	
	//Updates zooming input
	void UpdateZooming(){
		//Check right mouse button
		if(Input.GetMouseButtonDown(1)){
			zoomingAcitve = true;
			lastPosition = Input.mousePosition;
		}
		
		//Update zoom based on x mouse movement
		if(zoomingAcitve){
			float amount = Input.mousePosition.x - lastPosition.x;
			camControl.Zoom(amount * zoomingSpeed);
			lastPosition = Input.mousePosition;
		}
		
		//Reset zoom flag when right mouse button released
		if(Input.GetMouseButtonUp(1)){
			zoomingAcitve = false;
		}
	}
	
	//Called when left mouse button pressed on the object
	void OnMouseDown(){
		//Disable movement while zooming
		if(!zoomingAcitve){
			lastPosition = Input.mousePosition;
		}
	}
	
	//Called when mouse dragged on object with left button
	void OnMouseDrag(){
		//Disable movement while zooming
		if(!zoomingAcitve){
            Vector2 movement = lastPosition - (Vector2)Input.mousePosition;
			camControl.Move(movement * movementSpeed);
			lastPosition = Input.mousePosition;
		}
	}
}
