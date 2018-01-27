using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour {
    public const int STATE_OFF = 0;
    public const int STATE_ON  = 1;

    private int state = 0;
    private int currX;
    private int currY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetState(int aState){
        state = aState;
    }

    public int GetState(){
        return state;
    }
}
