using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    //Whether player has won the game
    private bool playerWon;

	// Use this for initialization
	void Start () {
        playerWon = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Receives EnemyDestroyed message
    //whenever an emeny has been destroyed
    void EnemyDestroyed()
    {
        //Count remaining enemies in the scene
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length <= 1)
        {
            //Player destroyed all enemies
            SendMessage("PlayerWon");
            playerWon = true;
        }
    }

    //Receives the message sent when player
    //has no more projectiles remaining
    void ProjectilesConsumed()
    {
        if (!playerWon)
        {
            SendMessage("PlayerLost");
        }
    }

    //Receives NewLevelLoaded message
    //and resets player win flag
    void NewLevelLoaded()
    {
        playerWon = false;
    }
}
