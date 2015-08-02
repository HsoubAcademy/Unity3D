using UnityEngine;
using System.Collections;

public class PathDrawer : MonoBehaviour {

    //Prefab used to build path points
    public GameObject pathPointPrefab;

    //Distance between two consecutive points
    public float pointDistance = 0.75f;

    //Parent of path game objects
    Transform pathParent;

    //Stores the position of the last added point
    Vector2 lastPointPosition;

    //Internal flag to know if the projectile is launched
    bool launched = false;

	// Use this for initialization
	void Start () {
	    //Find path parent
        pathParent = GameObject.Find("Path").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (launched)
        {
            float dist = Vector2.Distance(transform.position, lastPointPosition);
            if (dist >= pointDistance)
            {
                //Time to add new point
                AddPathPoint();
            }
        }
	}

    void ProjectileLaunched()
    {
        //Clear previous path
        for (int i = 0; i < pathParent.childCount; i++)
        {
            Destroy(pathParent.GetChild(i).gameObject);
        }

        AddPathPoint();

        //Set the flag
        launched = true;
    }

    //Adds new path point at the current position
    void AddPathPoint()
    {
        //Instantiate first point
        GameObject newPoint = (GameObject)Instantiate(pathPointPrefab);
        //Put it in the same position of the projectile
        newPoint.transform.position = transform.position;
        //Set the parent
        newPoint.transform.parent = pathParent;
        //Store its position
        lastPointPosition = transform.position;
    }
}
