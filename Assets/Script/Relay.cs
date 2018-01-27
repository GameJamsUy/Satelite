﻿using System.Collections;
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

    public int GetX()
    {
        return currX;
    }

    public int GetY()
    {
        return currY;
    }

    public void SetX(int aX){
        if (aX >= GameConstants.minX && aX <= GameConstants.maxX){
            transform.position = new Vector2(GameConstants.stepX * (aX + 1), transform.position.y);
            currX = aX;
        }
    }

    public void SetY(int aY){
        if (aY >= GameConstants.minY && aY <= GameConstants.maxY){
            transform.position = new Vector2(transform.position.x, GameConstants.stepY * (aY + 1));
            currY = aY;
        }
    }

    public void SetState(int aState){
        state = aState;
    }

    public int GetState(){
        return state;
    }
}
