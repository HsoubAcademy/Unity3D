using UnityEngine;
using System.Collections;

public class GameStateReporter : MonoBehaviour {

    //Reference to the game state dialogs
    GameStateDialogs gsDialogs;

	// Use this for initialization
	void Start () {
        gsDialogs = FindObjectOfType<GameStateDialogs>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Handles player won message
    void PlayerWon()
    {
        gsDialogs.ShowWinDialog();
    }

    //Handles player lost message
    void PlayerLost()
    {
        gsDialogs.ShowLoseDialog();
    }
}
