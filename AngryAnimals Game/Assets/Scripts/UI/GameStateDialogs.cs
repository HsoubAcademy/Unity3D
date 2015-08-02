using UnityEngine;
using System.Collections;

public class GameStateDialogs : MonoBehaviour {

    //Dialog shown when the player wins
    public UIDialog winDialog;

    //Dialog show when the player loses
    public UIDialog loseDialog;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Shows win dialog
    public void ShowWinDialog()
    {
        winDialog.Show();
    }

    //Shows lose dialog
    public void ShowLoseDialog()
    {
        loseDialog.Show();
    }
}
