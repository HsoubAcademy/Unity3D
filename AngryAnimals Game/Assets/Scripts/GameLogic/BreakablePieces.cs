using UnityEngine;
using System.Collections;

public class BreakablePieces : MonoBehaviour {
	
	//Prefab of the pieces
	public GameObject piecePrefab;
	
	//Sprites to use for pieces
	public Sprite[] piecesSprites;
	
	//How many pieces must be created
	public int pieceCount;
	
	//Scale factor for created pieces
	public float scale = 1.0f;
	
	//How strong is the generated explosion?
	//zero = no explosion
	public float explosionForce = 0.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Receives BreakableDestroyed message
	public void BreakableDestroyed(){
		//Get the collider of the object
		Collider2D myCollider;
		myCollider = GetComponent<Collider2D>();
		
		//Find collider's bounds
		Bounds myBounds = myCollider.bounds;
		
		//Instantiate the desired number of pieces
		for(int i = 0; i < pieceCount; i++){
			GameObject piece = (GameObject) Instantiate(piecePrefab);
			
			//Set the sprite
			int index = i % piecesSprites.Length;
			SpriteRenderer sr = piece.GetComponent<SpriteRenderer>();
			sr.sprite = piecesSprites[index];
			
			//Generate random position within the bounds
			float posX = Random.Range(myBounds.min.x, myBounds.max.x);
			float posY = Random.Range(myBounds.min.y, myBounds.max.y);
			
			//Put the piece in the random position
			piece.transform.position = new Vector2(posX, posY);
			
			//Make the parent of the original shape
			//the parent of broken pieces
			piece.transform.parent = transform.parent;
			
			//Scale the piece
			piece.transform.localScale *= scale;
			
			//Get the rigid body of the piece
			Rigidbody2D rb = piece.GetComponent<Rigidbody2D>();
			
			//Add small force to move the piece
			//Force direction starts from the center of the
			//original shape
			Vector2 force = piece.transform.position - transform.position;
			if(explosionForce == 0.0f){
				//No explosion
				rb.AddForce (force, ForceMode2D.Impulse);
			} else {
				//There is explosion
				rb.AddForce(force * explosionForce, ForceMode2D.Impulse);
			}

            //Destroy piece after 5 seconds
            Destroy(piece, 5.0f);
		}

        if (explosionForce > 0.0f)
        {
            //Explosion effect on other breakables
            //Compute the radius of the explosion
            float radius = myBounds.max.magnitude * 2.0f;
            //Find all breakables in the scene
            Breakable[] breakables = FindObjectsOfType<Breakable>();
            //Add explosion force to breakables within explosion radius
            foreach (Breakable target in breakables)
            {
                //Exclude self
                if (target.gameObject != gameObject)
                {
                    //Find distance and compare it with radius
                    float dist = Vector2.Distance(
                        transform.position, target.transform.position);
                    if (dist <= radius)
                    {
                        //Apply damage to the target
                        target.TakeDamage(explosionForce / dist);
                        //Compute explosion force direction and amount
                        Vector2 expDir = target.transform.position - transform.position;
                        expDir = expDir * (explosionForce / radius);
                        //Apply explosion force
                        Rigidbody2D targetRB = target.GetComponent<Rigidbody2D>();
                        targetRB.AddForce(expDir, ForceMode2D.Impulse);
                    }
                }
            }
        }
	}
}
