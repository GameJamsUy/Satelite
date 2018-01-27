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

        //Debug.DrawRay(touchPosN, touchPosF - touchPosN, Color.red , 3.0f);
        RaycastHit2D hit = Physics2D.Raycast(touchPosN, touchPosF - touchPosN);
        if (hit.collider != null) {
            Lever _lever = hit.collider.GetComponent<Lever>();
            if (_lever != null) {
                _lever.LeverGetsClicked();
            }         
        }
    }




}
