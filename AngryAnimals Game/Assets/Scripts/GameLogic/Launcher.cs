using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {

    //Launch power of this launcher
    public float launchForce = 1.0f;
    
    //Maximum length the launcher can stretch to
    public float maxStretch = 1.0f;

    //Put projectiles on this position before holding
    public Transform launchPosition;

    //Current projectile on the launcher
    public Projectile currentProjectile;

    //Did we consume all projectiles in the scene?
    private bool projectilesConsumed = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (projectilesConsumed)
        {
            //Nothing to do
            return;
        }

        if (currentProjectile != null)
        {
            //If the current projectile is neither held nor launched,
            //then bring it towards the launch position.
            if (!currentProjectile.IsHeld() && !currentProjectile.IsLaunched())
            {
                BringCurrentProjectile();
            }
        }
        else
        {
            //There is not current projetcile
            //Get the nearest one to the launcher
            currentProjectile = GetNearestProjectile();
            if (currentProjectile == null)
            {
                //All projectiles were consumed, send a message about that
                projectilesConsumed = true;
                SendMessageUpwards("ProjectilesConsumed");
            }
        }
	}

    //Returns the nearest projectile
    Projectile GetNearestProjectile()
    {
        Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
        
        if (allProjectiles.Length == 0)
        {
            //No more projectiles remain
            return null;
        }

        //Find the nearest projectile and return it
        Projectile nearest = allProjectiles[0];
        float minDist = Vector2.Distance(nearest.transform.position, transform.position);

        for (int i = 1; i < allProjectiles.Length; i++)
        {
            float dist = Vector2.Distance(allProjectiles[i].transform.position, transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = allProjectiles[i];
            }
        }

        return nearest;
    }

    //Moves current projectile one step smoothly towards launcher's position
    void BringCurrentProjectile()
    {
        //Get positions 
        Vector2 projectilePos = currentProjectile.transform.position;
        Vector2 launcherPos = launchPosition.transform.position;

        if (projectilePos == launcherPos)
        {
            //Already in launch position, nothing to do
            return;
        }
        //Use linear interpolation along with delta time for smooth movement
        projectilePos = Vector2.Lerp(projectilePos, launcherPos, Time.deltaTime * 5.0f);
        //Put the projectile in its new position
        currentProjectile.transform.position = projectilePos;

        if (Vector2.Distance(launcherPos, projectilePos) < 0.1f)
        {
            //Projectile became so close, put it directly in launch position
            currentProjectile.transform.position = launcherPos;
            currentProjectile.AllowControl();
        }
    }

    //Holds the current projectile
    public void HoldProjectile()
    {
        if (currentProjectile != null)
        {
            currentProjectile.Hold();
        }
    }

    //Drags the current projectile to a new position
    public void DragProjectile(Vector2 newPosition)
    {
        
        if (currentProjectile != null)
        {
            //Enforce stretch amount
            float currentDist = Vector2.Distance(newPosition, launchPosition.position);
            
            if (currentDist > maxStretch)
            {
                //Move the new position to the farthest possible point
                float lerpAmount = maxStretch / currentDist;
                newPosition = Vector2.Lerp(launchPosition.position, newPosition, lerpAmount);
            }

            //Set the new position
            currentProjectile.Drag(newPosition);
        }
                
    }

    //Releases the current projectile if the player is holding it
    public void ReleaseProjectile()
    {
        if (currentProjectile != null)
        {
            currentProjectile.Launch(launchForce);
        }
    }
}
