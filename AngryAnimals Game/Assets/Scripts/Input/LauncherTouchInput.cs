using UnityEngine;
using System.Collections;

public class LauncherTouchInput : MonoBehaviour {

    //Reference to the launcher
    private Launcher launcher;

	// Use this for initialization
	void Start () {
        //Destroy mouse input if Android detected
        if (Application.platform == RuntimePlatform.Android)
        {
            LauncherMouseInput mouseInput = GetComponent<LauncherMouseInput>();
            Destroy(mouseInput);
        }

        launcher = GetComponent<Launcher>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTouchStart();
        UpdateDragging();
        UpdateRelease();
	}

    //Checks if the player touched the projectile with 
    //one finger
    void UpdateTouchStart()
    {
        Projectile currentProj = launcher.currentProjectile;
        if (currentProj == null)
        {
            //Nothing to do
            return;
        }

        if (Input.touchCount == 1)
        {
            Touch playerTouch = Input.touches[0];
            if (playerTouch.phase == TouchPhase.Began)
            {
                //Player has just touch the screen
                //Check if the touch was on the projectile
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(playerTouch.position);

                //Get projectile's collider
                Collider2D projectileCollider = currentProj.GetComponent<Collider2D>();

                if (projectileCollider.bounds.Contains(touchPos))
                {
                    //Touch was inside projectile, must hold
                    launcher.HoldProjectile();
                }
            }
        }
    }

    //Updates player drag with one finger
    void UpdateDragging()
    {
        if (Input.touchCount == 1)
        {
            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            launcher.DragProjectile(touchWorldPos);
        }
    }

    //Checks if the player removed his finger from screen
    void UpdateRelease()
    {
        if (Input.touchCount == 1)
        {
            Touch playerTouch = Input.touches[0];
            if (playerTouch.phase == TouchPhase.Ended ||
                playerTouch.phase == TouchPhase.Canceled)
            {
                launcher.ReleaseProjectile();
            }
        }
    }
}
