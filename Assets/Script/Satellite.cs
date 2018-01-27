using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour {

    public static readonly int STATE_IDLE       = 0;
    public static readonly int STATE_TURNING    = 1;

    public static readonly int COLOR_RED   = 0;
    public static readonly int COLOR_GREEN = 1;
    public static readonly int COLOR_BLUE  = 2;

    public static readonly float[] ANGLES       = { 0, 90, 180, 270 };
    public static readonly float ANGLE_UP       = 0;
    public static readonly float ANGLE_RIGHT    = 90;
    public static readonly float ANGLE_DOWN     = 180;
    public static readonly float ANGLE_LEFT     = 270;

    public const int ROTATE_RIGHT     = 0;
    public const int ROTATE_LEFT      = 1;
    public const int ROTATE_INVERT    = 2;
    public const int ROTATE_FULL_TURN = 3;

    private int color;
    private int angle;
    private float angleDeg;
    private int state;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            Rotate(ROTATE_LEFT);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)){
            Rotate(ROTATE_RIGHT);
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
        
        while (currDeg > targetDeg + 3){
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

        while (currDeg < targetDeg - 3){
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
    }

    public float GetAngleDegrees(){
        return angleDeg;
    }

    public void SetAngleDegrees(float aAngle){
        angleDeg = aAngle;
    }

    public void Rotate(int satelliteRotation){
        if(state == STATE_IDLE){
            SetState(STATE_TURNING);
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

    public void SetState(int aState){
        state = aState;
    }

    public int GetState(){
        return state;
    }
}
