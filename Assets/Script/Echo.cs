using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour {
    public const int STATE_OFF = 0;
    public const int STATE_ON  = 1;

    public Sprite[] onOffFrames;
    private int state = 0;
    private int currX;
    private int currY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetState() == STATE_OFF && IsBeingActivated()){
            SetState(STATE_ON);
        }
        else if(GetState() == STATE_ON && !IsBeingActivated()){
            SetState(STATE_OFF);
        }
    }

    public int GetX()
    {
        return currX;
    }

    public int GetY()
    {
        return currY;
    }

    public void SetX(int aX) {
        if (aX >= GameConstants.minX && aX <= GameConstants.maxX) {
            transform.position = new Vector2(GameConstants.startX + GameConstants.stepX * (aX), transform.position.y);
            currX = aX;
        }
    }

    public void SetY(int aY) {
        if (aY >= GameConstants.minY && aY <= GameConstants.maxY) {
            transform.position = new Vector2(transform.position.x, GameConstants.startY + GameConstants.stepY * (aY));
            currY = aY;
        }
    }

    public void SetState(int aState){
        state = aState;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(state == STATE_ON){
            sr.sprite = onOffFrames[1];
        }
        else{
            sr.sprite = onOffFrames[0];
        }
    }

    public int GetState(){
        return state;
    }

    public bool IsBeingActivated(){
        int triggers = 0;
        foreach (Satellite currSat in Manager.GetSatellites()){
            if(currSat.GetY() == GetY() && currSat.GetX() == GetX() - 1 && currSat.GetAngleDegrees() == Satellite.ANGLE_RIGHT){
                //Debug.Log("echo activated by left sat pointing right");
                triggers++;
            }
            if (currSat.GetY() == GetY() && currSat.GetX() == GetX() + 1 && currSat.GetAngleDegrees() == Satellite.ANGLE_LEFT){
                //Debug.Log("echo activated by right sat pointing left");
                triggers++;
            }            
        }
        foreach(Relay currRelay in Manager.GetRelays()){
            if (currRelay.GetY() == GetY() && currRelay.GetX() == GetX() - 1 && currRelay.GetState() == Relay.STATE_CONNECTED){
                //Debug.Log("echo activated by left relay on");
                triggers++;
            }
            if (currRelay.GetY() == GetY() && currRelay.GetX() == GetX() + 1 && currRelay.GetState() == Relay.STATE_CONNECTED){
                //Debug.Log("echo activated by right relay on");
                triggers++;
            }
        }
        if(triggers >= 2){
            return true;
        }
        return false;
    }
}
