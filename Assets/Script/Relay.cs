using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour {
    public const int STATE_DISCONNECTED = 0;
    public const int STATE_CONNECTED    = 1;

    public Sprite[] onOffFrames;
    private int state = 0;
    private int currX;
    private int currY;

    private bool fromDown = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GetState() == STATE_DISCONNECTED && IsBeingActivated()){
            SetState(STATE_CONNECTED);
        }
        else if (GetState() == STATE_CONNECTED && !IsBeingActivated()){
            SetState(STATE_DISCONNECTED);
        }
	}


    public void MoveVertical() {
        if (currY == 2 && fromDown == false) {
            MoveDown();
        }
        else if (currY == 2 && fromDown == true) {
            MoveUp();
            fromDown = false;
        }
        else if (currY == 3) {
            MoveDown();
            fromDown = false;
        }
        else if (currY == 1) {
            MoveUp();
            fromDown = true;
        }
        
    } 


    public void MoveUp(){
        if(currY < GameConstants.maxY){
            float targetY = transform.position.y + GameConstants.stepY;
            StartCoroutine(MoveToY(targetY));
        }
        currY += 1;
    }

    public void MoveDown(){
        if (currY > GameConstants.minY){
            float targetY = transform.position.y - GameConstants.stepY;
            StartCoroutine(MoveToY(targetY));
        }
        currY -= 1;
    }

    private IEnumerator MoveToX(float targetX){
        float totalTime = 1;
        float currTime = 0;
        float startPos = transform.position.x;
        while(currTime < totalTime){
            currTime += Time.deltaTime;
            float t = currTime / totalTime;
            transform.position = new Vector3(Mathf.Lerp(startPos, targetX, t), transform.position.y);
            yield return null;
        }
        yield return null;
    }

    private IEnumerator MoveToY(float targetY){
        float totalTime = 1;
        float currTime = 0;
        float startPos = transform.position.y;
        while (currTime < totalTime){
            currTime += Time.deltaTime;
            float t = currTime / totalTime;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startPos, targetY, t));
            yield return null;
        }
        yield return null;
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
        if (state == STATE_CONNECTED){
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
        foreach (Satellite currSat in Manager.GetSatellites()){
            if (currSat.GetY() == GetY() && currSat.GetX() == GetX() - 1 && currSat.GetAngleDegrees() == Satellite.ANGLE_RIGHT){
                //Debug.Log("relay activated by left sat pointing right");
                return true;
            }
            if (currSat.GetY() == GetY() && currSat.GetX() == GetX() + 1 && currSat.GetAngleDegrees() == Satellite.ANGLE_LEFT){
                //Debug.Log("relay activated by right sat pointing left");
                return true;
            }
        }

        return false;
    }
}
