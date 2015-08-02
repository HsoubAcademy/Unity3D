using UnityEngine;
using System.Collections;

public class CameraTouchInput : MonoBehaviour
{

    //Movement speed when dragging
    public float movementSpeed = 0.0075f;

    //Zooming speed when touching with two fingers
    public float zoomingSpeed = 0.00075f;

    //Whether the player touches the screen with single finger
    private bool singleTouch = false;

    //Whether the player touches the screen with two fingers
    private bool doubleTouch = false;

    //Reference to the attached camera controller
    CameraControl camControl;

    // Use this for initialization
    void Start()
    {
        //Destroy mouse input if Android detected
        if (Application.platform == RuntimePlatform.Android)
        {
            CameraMouseInput mouseInput = GetComponent<CameraMouseInput>();
            Destroy(mouseInput);
        }

        camControl = FindObjectOfType<CameraControl>();
    }

    // LateUpdate is called later than Update
    void LateUpdate()
    {
        UpdateSingleTouch();
        UpdateDragging();
        UpdateDoubleTouch();
        UpdateZooming();
    }

    //Scans for single touch by the player
    void UpdateSingleTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch playerTouch = Input.touches[0];
            if (playerTouch.phase == TouchPhase.Began)
            {
                //Player has just touched the screen
                //Was it on this background element?
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(playerTouch.position);

                //Get the collider of this background element
                Collider2D myCollider = GetComponent<Collider2D>();

                //Generate very short ray from touch position to see if it 
                //collides with this background element
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.one, 0.1f);
                if (hit.collider == myCollider)
                {
                    //Yes, the touch was on this background element
                    singleTouch = true;
                }
            }
            else if (playerTouch.phase == TouchPhase.Ended ||
              playerTouch.phase == TouchPhase.Canceled)
            {
                //Player has just removed his finger from the screen
                singleTouch = false;
            }
        }
        else
        {
            //Number of fingers on screen is not 1
            //no single touch
            singleTouch = false;
        }
    }

    //Checks for dragging with single input
    void UpdateDragging()
    {
        if (singleTouch)
        {
            Touch playerTouch = Input.touches[0];
            camControl.Move(playerTouch.deltaPosition * -movementSpeed);
        }
    }

    //Checks for two-finger touch
    void UpdateDoubleTouch()
    {
        //Make sure there is no active single touch
        if (!singleTouch)
        {
            if (Input.touchCount == 2)
            {
                doubleTouch = true; 
            }
            else
            {
                doubleTouch = false;
            }
        }
    }

    //Updates zooming input with two fingers
    void UpdateZooming()
    {
        if (doubleTouch)
        {
            //Positions of the two fingers A and B
            //in both current and previous frames
            Vector2 posA1, posA2, posB1, posB2;
            Touch a, b;
            a = Input.touches[0];
            b = Input.touches[1];

            posA2 = a.position;
            posB2 = b.position;

            posA1 = a.position - a.deltaPosition;
            posB1 = b.position - b.deltaPosition;

            //Check if distance between fingers increased
            //or decreased since the last frame
            float currentDist = Vector2.Distance(posA2, posB2);
            float prevDist = Vector2.Distance(posA1, posB1);

            //Subtracting previous distance from current 
            //gives us the correct sign for zooming value
            camControl.Zoom((currentDist - prevDist) * zoomingSpeed);
        }
    }
}
