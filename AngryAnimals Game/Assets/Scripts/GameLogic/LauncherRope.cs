using UnityEngine;
using System.Collections;

public class LauncherRope : MonoBehaviour {

    //Left end of the rope
    public Transform leftEnd;

    //Right end of the rope
    public Transform rightEnd;

    //Reference to the attached launcher
    Launcher launcher;

    //Reference to the attached line renderer
    LineRenderer line;

	// Use this for initialization
	void Start () {
        launcher = GetComponent<Launcher>();
        line = GetComponent<LineRenderer>();
        //Hide the line at the beginning by disabling its component
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Show the line only when the projectile is held
        if (launcher.currentProjectile != null && 
            launcher.currentProjectile.IsHeld())
        {
            if (!line.enabled)
            {
                line.enabled = true;
            }
            //Draw the line between left end, projectile, and right end
            line.SetPosition(0, leftEnd.position);
            line.SetPosition(1, launcher.currentProjectile.transform.position);
            line.SetPosition(2, rightEnd.position);
        }
        else
        {
            line.enabled = false;
        }
	}
}
