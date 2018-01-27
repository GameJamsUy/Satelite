using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour {
    public const int STATE_DISCONNECTED = 0;
    public const int STATE_CONNECTED    = 1;

    private int state = 0;
    private int currX;
    private int currY;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveUp(){
        if(currY < GameConstants.maxY){
            transform.position = new Vector2(transform.position.x, transform.position.y + GameConstants.stepY);
        }
        currY += 1;
    }

    public void MoveDown(){
        if (currY > GameConstants.minY){
            transform.position = new Vector2(transform.position.x, transform.position.y - GameConstants.stepY);
        }
        currY -= 1;
    }

    public void SetState(int aState){
        state = aState;
    }

    public int GetState(){
        return state;
    }
}
