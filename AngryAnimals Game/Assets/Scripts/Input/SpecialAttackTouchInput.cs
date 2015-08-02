using UnityEngine;
using System.Collections;

public class SpecialAttackTouchInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Destroy mouse input if Android detected
        if (Application.platform == RuntimePlatform.Android)
        {
            SpecialAttackMouseInput mouseInput = GetComponent<SpecialAttackMouseInput>();
            Destroy(mouseInput);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1)
        {
            Touch playerTouch = Input.touches[0];
            if(playerTouch.phase == TouchPhase.Began)
            {
                SendMessage("PerformSpecialAttack");
            }
        }
	}
}
