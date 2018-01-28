using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Satellite : MonoBehaviour {

    public static readonly int STATE_IDLE       = 0;
    public static readonly int STATE_TURNING    = 1;

    public const int COLOR_RED   = 0;
    public const int COLOR_GREEN = 1;
    public const int COLOR_BLUE  = 2;

    public static readonly float[] ANGLES       = { 0, 90, 180, 270 };
    public static readonly float ANGLE_UP       = 0;
    public static readonly float ANGLE_RIGHT    = 270;
    public static readonly float ANGLE_DOWN     = 180;
    public static readonly float ANGLE_LEFT     = 90;

    public const int ROTATE_RIGHT     = 0;
    public const int ROTATE_LEFT      = 1;
    public const int ROTATE_INVERT    = 2;
    public const int ROTATE_FULL_TURN = 3;

    public Sprite[] colors;
    private bool transmitting;
    public GameObject transmittingParticle;
    private int color;
    private int angle;
    private float angleDeg;
    private int currX;
    private int currY;
    private int state;

    //private SoundScript _soundRef;


	// Use this for initialization
	void Start () {
        //_soundRef = transform.root.GetComponent<SceneSwitcher>().soundRef;
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsTransmitting() && CheckTransmission()){
            SetTransmitting(true);
        }
        else if(IsTransmitting() && !CheckTransmission()){
            SetTransmitting(false);
        }
    }

    private IEnumerator RotateRight(float degrees){

        //totalmente desprolijo por estar apurado
        float targetDeg = angleDeg + degrees;
        float currDeg = angleDeg;
        float totalTime = 1f;
        float currTime = 0;

        if (currDeg == 0){
            currDeg = 360;
        }

        if(targetDeg >= 360){
            targetDeg -= 360;
        }
        
        while (currDeg > targetDeg + 6){
            //gameObject.GetComponentInChildren<Text>().text = "TgtDg: " + targetDeg + " / " + "anglDeg: " + angleDeg;
            gameObject.GetComponentInChildren<Text>().text = "";
            currTime += Time.deltaTime;
            float t = currTime / totalTime;
            currDeg = transform.eulerAngles.z;
            if (currDeg <= 0){
                currDeg += 360;
            }
            transform.eulerAngles = new Vector3(0, 0, angleDeg - (degrees * t));
            yield return null;
        }

        if (targetDeg == 360){
            targetDeg = 0;
        }
        angleDeg = targetDeg;
        transform.eulerAngles = new Vector3(0, 0, angleDeg);
        SetState(STATE_IDLE);
        yield return null;
    }

    private IEnumerator RotateLeft(float degrees){
        //totalmente desprolijo por estar apurado
        float targetDeg = angleDeg + degrees;
        float currDeg = angleDeg;
        float totalTime = 1f;
        float currTime = 0;
        if (targetDeg <= 0){
            targetDeg += 360;
        }

        if (currDeg == 360){
            currDeg = 0;
        }

        while (currDeg < targetDeg - 6){
            currTime += Time.deltaTime;
            currDeg = transform.eulerAngles.z;
            float t = currTime / totalTime;
            transform.eulerAngles = new Vector3(0, 0, angleDeg - (degrees * t));
            yield return null;
        }

        angleDeg = targetDeg;
        transform.eulerAngles = new Vector3(0, 0, angleDeg);
        SetState(STATE_IDLE);
        yield return null;
    }

    public int GetColor(){
        return color;
    }

    public void SetColor(int aColor){
        color = aColor;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = colors[color];
    }

    public float GetAngleDegrees(){
        return angleDeg;
    }

    public void SetAngleDegrees(float aAngle){
        angleDeg = aAngle;
    }

    public void SetStartingRotation(float angle) {
        angleDeg = angle;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
    }

    public void Rotate(int satelliteRotation){
        if(state == STATE_IDLE){
            SetState(STATE_TURNING);
            //_soundRef.PlayShortSound(SoundScript.SoundType.SATELLITE_ROTATE);
            switch (satelliteRotation){
                case ROTATE_RIGHT:
                    StartCoroutine(RotateRight(270));
                    break;
                case ROTATE_LEFT:
                    StartCoroutine(RotateLeft(-270));
                    break;
                case ROTATE_INVERT:
                    StartCoroutine(RotateRight(180));
                    break;
                case ROTATE_FULL_TURN:
                    StartCoroutine(RotateRight(360));
                    break;
                default:
                    break;
            }
        }
    }

    public int GetX(){
        return currX;
    }

    public int GetY(){
        return currY;
    }

    public void SetX(int aX){
        if(aX >= GameConstants.minX && aX <= GameConstants.maxX){
            transform.position = new Vector2(GameConstants.startX + GameConstants.stepX * (aX), transform.position.y);
            currX = aX;
        }
    }

    public void SetY(int aY){
        if (aY >= GameConstants.minY && aY <= GameConstants.maxY){
            transform.position = new Vector2(transform.position.x, GameConstants.startY + GameConstants.stepY * (aY));
            currY = aY;
        }
    }

    public void SetState(int aState){
        state = aState;
    }

    public int GetState(){
        return state;
    }

    public void SetTransmitting(bool value){
        transmitting = value;
        if (transmitting){
            //_soundRef.PlayShortSound(SoundScript.SoundType.CONNECTION_ESTABLISHED);
            transmittingParticle.SetActive(true);
        }
        else{
            transmittingParticle.SetActive(false);
        }
    }

    public bool IsTransmitting(){
        return transmitting;
    }

    private bool CheckTransmission(){
        foreach (City currCity in Manager.GetCities()){
            if (currCity.GetX() == GetX() && angleDeg == ANGLE_DOWN){
                return true;
            }
        }

        foreach (Relay currRelay in Manager.GetRelays()){
            if (currRelay.GetX() == GetX() - 1 && currRelay.GetY() == GetY() && angleDeg == ANGLE_LEFT
             || currRelay.GetX() == GetX() + 1 && currRelay.GetY() == GetY() && angleDeg == ANGLE_RIGHT){
                return true;
            }
        }

        foreach (Echo currEcho in Manager.GetEchos()){
            if (currEcho.GetX() == GetX() - 1 && angleDeg == ANGLE_LEFT || currEcho.GetX() == GetX() + 1 && angleDeg == ANGLE_RIGHT){
                return true;
            }
        }
        return false;
    }
}
