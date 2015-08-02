using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Starts the game with the specified level
    public void StartLevel(int levelIndex)
    {
        //Set the provided level index as selected
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
        //Load game level
        Application.LoadLevel(1);
    }
}
