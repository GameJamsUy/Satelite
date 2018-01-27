using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMouseBasedControl();
    }

    private void UpdateMouseBasedControl() {
        if (Input.GetMouseButtonUp(0)) {
            UseMousePos(Input.mousePosition);
        }
    }

    private void UseMousePos(Vector3 inputPos) {
        //Needed to cast rays for collision detection in perspective mode
        Vector3 touchPosFar = new Vector3(inputPos.x, inputPos.y, Camera.main.farClipPlane);
        Vector3 touchPosNear = new Vector3(inputPos.x, inputPos.y, Camera.main.nearClipPlane);

        //Needed to cast rays for collision detection in perspective mode
        Vector3 touchPosF = Camera.main.ScreenToWorldPoint(touchPosFar);
        Vector3 touchPosN = Camera.main.ScreenToWorldPoint(touchPosNear);

        //User clicks on lever during play 
        RaycastHit2D hit = Physics2D.Raycast(touchPosN, touchPosF - touchPosN);
        if (hit.collider != null) {
            Lever _lever = hit.collider.GetComponent<Lever>();
            if (_lever != null) {
                _lever.LeverGetsClicked();
            }         
        }

        //User clicks on Credits button in start menu
        if (hit.collider != null && hit.collider.name == ("CreditsButton")) {
            StartScreenManager startScreen = transform.GetComponent<StartScreenManager>();
            startScreen.CreditsClick();
        }


        //User clicks on back button in credits
        if (hit.collider != null && hit.collider.name == ("BackButton")) {
            StartScreenManager startScreen = transform.GetComponent<StartScreenManager>();
            startScreen.BackCreditsClick();
        }


        //User clicks on play button in start menu
        if (hit.collider != null && hit.collider.name == ("StartGameButton")) {
            StartScreenManager startScreen = transform.GetComponent<StartScreenManager>();
            startScreen.PlayButtonClick();
        }

    }




}
