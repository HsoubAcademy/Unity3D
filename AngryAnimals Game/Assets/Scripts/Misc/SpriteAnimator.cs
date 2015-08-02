using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour {

    //Frames of the animation
    public Sprite[] frames;

    //Speed of the animation
    public float framesPerSecond = 16.0f;

    //Should the game object be destroyed on completion?
    public bool destroyOnCompletion = true;

    //Last frame change time
    private float lastChange = 0.0f;

    //Reference to the attached renderer
    private SpriteRenderer renderer;

    //Index of the current frame
    private int currentFrame = 0;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Animate();
	}

    //Animates the sprite by switching frames
    void Animate()
    {
        //Duration of a single frame
        float frameTime = 1.0f / framesPerSecond;
        //Switch to next frame if frame time passed
        if (Time.time - lastChange > frameTime)
        {
            lastChange = Time.time;
            currentFrame = (currentFrame + 1) % frames.Length;
            renderer.sprite = frames[currentFrame];
            if (currentFrame == 0 && destroyOnCompletion)
            {
                Destroy(gameObject);
            }
        }
    }
}
