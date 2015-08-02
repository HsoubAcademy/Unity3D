using UnityEngine;
using System.Collections;

public class GameLevelLoader : MonoBehaviour {

    //Stores prefabs of all levels
    public GameLevel[] allLevels;

    //Index of the currently loaded level
    private int currentLevel = -1;

    //Reference to the background manager
    private BackgroundManager bgManager;

    //Reference to exit game command
    ExitGameCommand egc;

	// Use this for initialization
	void Start () {
        bgManager = GetComponent<BackgroundManager>();
        egc = FindObjectOfType<ExitGameCommand>();
	}

	// LateUpdate is called later than Update
	void LateUpdate () {
        if (currentLevel == -1)
        {
            //Load the level selected from the main menu
            //Load the first level if no level selected
            int selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 0);
            LoadLevel(selectedLevel);
        }
	}

    //Loads the selected level
    public void LoadLevel(int index)
    {
        //Make sure if the selected level exists,
        //if not, go to main menu
        if (index < 0 || index >= allLevels.Length)
        {
            egc.ExitGame();
            return;
        }

        //Find current level and destroy it
        GameLevel current = FindObjectOfType<GameLevel>();
        if (current != null)
        {
            Destroy(current.gameObject);
        }

        //Instantiate the new level
        GameObject newLevelObject = (GameObject)Instantiate(allLevels[index].gameObject);
        GameLevel newLevelScript = newLevelObject.GetComponent<GameLevel>();

        //Set the parent and the position of the level
        newLevelObject.transform.parent = transform;
        newLevelObject.transform.position = Vector2.zero;

        //Change current level index
        currentLevel = index;

        //Change background to the new level
        bgManager.ChangeBackground(newLevelScript.backgroundIndex);

        //Inform about new level load
        SendMessage("NewLevelLoaded");
    }

    //Restarts the current level
    public void RestartCurrentLevel()
    {
        if (currentLevel != -1)
        {
            LoadLevel(currentLevel);
        }
    }

    //Loads the next level in levels array
    public void LoadNextLevel()
    {
        LoadLevel(currentLevel + 1);
    }
}
