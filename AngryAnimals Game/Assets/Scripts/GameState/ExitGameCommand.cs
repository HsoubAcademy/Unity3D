using UnityEngine;
using System.Collections;

public class ExitGameCommand : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Exits the game
    public void ExitGame()
    {
        if (Application.loadedLevel == 0)
        {
            //We are in the main menu, quit game application
            Application.Quit();
        }
        else if (Application.loadedLevel == 1)
        {
            //We are in game scene, exit to main menu
            Application.LoadLevel(0);
        }
    }
}
