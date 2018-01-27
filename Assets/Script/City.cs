using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public const int STATE_OFF = 0;
    public const int STATE_ON = 1;

    private int state;
    public Sprite[] onOffFrames;
    public GameObject onParticleSystem;
    public GameObject onParticleSystemSideways;
    private bool sidewaysTransmission;

    private int x;
    private int y;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetState() == STATE_OFF && IsPointedAt()){
            SetState(STATE_ON);
        }
        else if (GetState() == STATE_ON && !IsPointedAt()){
            SetState(STATE_OFF);
        }
    }

    public int GetState(){
        return state;
    }

    public void SetState(int aState){
        state = aState;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (state == STATE_ON){
            if (!sidewaysTransmission){
                onParticleSystem.SetActive(true);
            }
            else{
                onParticleSystem.SetActive(false);
                onParticleSystemSideways.SetActive(true);
            }
            sr.sprite = onOffFrames[1];
        }
        else{
            onParticleSystem.SetActive(false);
            onParticleSystemSideways.SetActive(false);
            sr.sprite = onOffFrames[0];
        }
    }

    public bool IsPointedAt(){
        foreach (Satellite currSat in Manager.GetSatellites()){
            if (currSat.GetX() == GetX() && currSat.GetAngleDegrees() == Satellite.ANGLE_DOWN){
                return true;
            }
        }

        foreach (Echo currEcho in Manager.GetEchos()){
            if (currEcho.GetX() == GetX() && currEcho.GetState() == Echo.STATE_ON){
                return true;
            }
            if (currEcho.GetX() + 1 == GetX() && currEcho.GetState() == Echo.STATE_ON){
                sidewaysTransmission = true;
                Debug.Log(sidewaysTransmission);
                return true;
            }
        }

        return false;
    }

    public int GetX(){
        return x;
    }

    public int GetY(){
        return y;
    }

    public void SetX(int aX){
        if (aX >= GameConstants.minX && aX <= GameConstants.maxX){
            transform.position = new Vector2(GameConstants.startX + GameConstants.stepX * (aX), transform.position.y);
            x = aX;
        }
    }

    public void SetY(int aY){
        if (aY >= GameConstants.minY && aY <= GameConstants.maxY){
            transform.position = new Vector2(transform.position.x, GameConstants.startY + 40 + GameConstants.stepY * (aY));
            y = aY;
        }
    }
}
