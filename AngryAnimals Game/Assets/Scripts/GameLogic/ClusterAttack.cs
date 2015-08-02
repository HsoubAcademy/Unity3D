using UnityEngine;
using System.Collections;

public class ClusterAttack : MonoBehaviour {

    //Prefab to generate clusters from
    public GameObject clusterPrefab;

    //How long should clusters live
    public float clusterLife = 4.0f;

    //To how many parts should the projectile be split?
    public int clusterCount = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Performs clustering attack
    public void DoSpecialAttack()
    {
        //Get the original velocity of the projectile
        Rigidbody2D myRB = GetComponent<Rigidbody2D>();
        float originalVelocity = myRB.velocity.magnitude;

        //Store all colliders of the newly created clusters
        Collider2D[] colliders = new Collider2D[clusterCount];
        Collider2D myCollider = GetComponent<Collider2D>();

        for (int i = 0; i < clusterCount; i++)
        {
            //Instantiate a new cluster
            GameObject cluster = (GameObject)Instantiate(clusterPrefab);
            //Set parent, name, and position
            cluster.transform.parent = transform.parent;
            cluster.name = name + "_cluster_" + i;
            cluster.transform.position = transform.position;
            //Store the collider
            colliders[i] = cluster.GetComponent<Collider2D>();
            //Ignore collisions between clusters each other
            //and between clusters and the original projectile
            Physics2D.IgnoreCollision(colliders[i], myCollider);
            for (int a = 0; a < i; a++)
            {
               Physics2D.IgnoreCollision(colliders[i], colliders[a]);
            }
            
            Vector2 clusterVelocity;
            //Each time we decrease x component and increase y component
            clusterVelocity.x = (originalVelocity / clusterCount) * (clusterCount - i);
            clusterVelocity.y = (originalVelocity / clusterCount) * -i;
            
            //Get the rigid body of the new cluster
            Rigidbody2D clusterRB = cluster.GetComponent<Rigidbody2D>();
            clusterRB.velocity = clusterVelocity;
            //Set the mass based on the mass of the original projectile
            clusterRB.mass = myRB.mass;
            //Destroy the cluster after its life time
            Destroy(cluster, clusterLife);
        }

        //Finally destroy the original projectile
        Destroy(gameObject);
    }
}
