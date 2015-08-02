using UnityEngine;
using System.Collections;

public class LauncherMouseInput : MonoBehaviour {

    //Reference to the launcher
    private Launcher launcher;

	// Use this for initialization
	void Start () {
        launcher = GetComponent<Launcher>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckButtonDown();
        CheckDragging();
        CheckButtonUp();
	}

    void CheckButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Left button was just clicked
            //Is there a projectile
            if (launcher.currentProjectile != null)
            {
                //Convert mouse position from screen to world coordinates
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Get the collider of the projectile
                Collider2D projectileCol = launcher.currentProjectile.GetComponent<Collider2D>();
                //Is mouse pointer inside projectile's collider bounds?
                if (projectileCol.bounds.Contains(mouseWorldPos))
                {
                    //Mouse held down over current projectile
                    //Hold the projectile
                    launcher.HoldProjectile();
                }
            }
        }
    }

    //Checks whether the player is dragging with the left mouse button
    void CheckDragging()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            launcher.DragProjectile(mouseWorldPos);
        }
    }

    //Checks whether left mouse button has been released
    void CheckButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Left mouse button was released
            //Try to launch the peojectile
            launcher.ReleaseProjectile();
        }
    }
}
