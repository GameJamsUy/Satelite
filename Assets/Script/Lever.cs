using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Lever : MonoBehaviour {
    public const int STATE_IDLE = 0;
    public const int STATE_MOVING = 1;
    private int state;

    public enum ActionType {ROTATE_RIGHT, ROTATE_LEFT, ROTATE_INVERT, ROTATE_FULL_TURN, MOVE_VERTICAL, NO_ACTION}

    public ActionInfo[] leverActions;
    public MainLevelScript mls;
    private Animator animator;

    private void Awake() {
        animator = transform.GetChild(0).GetComponent<Animator>();
        initialTransPos = transform.position;
    }

    void Start(){
        Manager.AddLever(this);
    }

    public Sprite[] buttonSprites;
    public float idlePos;
    public float endPos;
    private Vector2 initialTransPos;

    int red = 0;
    int green = 1;
    int blue = 2;

    public void LeverGetsClicked() {
        bool moveable = true;
        foreach (Lever currLever in Manager.GetLevers()){
            if(currLever.GetState() != STATE_IDLE){
                moveable = false;
            }
        }
        if(moveable){
            mls.SetCurrentMovements(mls.GetCurrentMovements() + 1);
            mls.SetActionsLeft(mls.GetActionsLeft() - 1);
            for (int i = 0; i < leverActions.Length; i++){
                PlayButtonAnimation();
                switch (leverActions[i].satTargetType)
                {
                    case MainLevelScript.SatTypes.COLOR_RED:
                        DoActionsOnAllSatsOfColor(i, red);
                        break;
                    case MainLevelScript.SatTypes.COLOR_GREEN:
                        DoActionsOnAllSatsOfColor(i, green);
                        break;
                    case MainLevelScript.SatTypes.COLOR_BLUE:
                        DoActionsOnAllSatsOfColor(i, blue);
                        break;
                    case MainLevelScript.SatTypes.RELAY:
                        DoActionsOnAllRelays(i);
                        break;
                    case MainLevelScript.SatTypes.ECHO:
                        DoActionsOnAllEchos(i);
                        break;
                    default:
                        break;
                }
            }
            SetState(STATE_MOVING);
        }
        else if(state == STATE_MOVING){
            //do nothing, wait
        }
    }

    void PlayButtonAnimation() {
        //animator.SetTrigger("buttonPress");
        StartCoroutine(MoveButtonDown());
    }

    private IEnumerator MoveButtonDown(){
        float totalTime = .15f;
        float currTime = 0;
        Vector2 pos = transform.position;
        float startPos = pos.y;
        float targetPos = startPos + endPos;
        while (currTime < totalTime){
            currTime += Time.deltaTime;
            float t = currTime / totalTime;
            transform.position = new Vector2(pos.x, Mathf.Lerp(startPos, targetPos, t));
            yield return null;
        }
        StartCoroutine(MoveButtonUp());
        yield return null;
    }
    private IEnumerator MoveButtonUp(){
        float totalTime = .65f;
        float currTime = 0;
        Vector2 pos = transform.position;
        float startPos = pos.y;
        float targetPos = initialTransPos.y;
        while (currTime < totalTime){
            currTime += Time.deltaTime;
            float t = currTime / totalTime;
            transform.position = new Vector2(pos.x, Mathf.Lerp(startPos, targetPos, t));
            yield return null;
        }
        yield return new WaitForSeconds(.3f);
        SetState(STATE_IDLE);
        yield return null;
    }

    void DoActionsOnAllSatsOfColor(int j, int color) {
        int numberOfSatellites = Manager.GetSatellites().Count;
        for (int i = 0; i < numberOfSatellites; i++) {
            if ((Manager.GetSatellites()[i].GetColor() == color)) {
                if (leverActions[j].actionType == ActionType.ROTATE_RIGHT) {
                    Manager.GetSatellites()[i].Rotate(Satellite.ROTATE_RIGHT);
                }
                else if (leverActions[j].actionType == ActionType.ROTATE_LEFT){
                    Manager.GetSatellites()[i].Rotate(Satellite.ROTATE_LEFT);
                }
                else if (leverActions[j].actionType == ActionType.ROTATE_INVERT) {
                    Manager.GetSatellites()[i].Rotate(Satellite.ROTATE_INVERT);
                }
                else if (leverActions[j].actionType == ActionType.ROTATE_FULL_TURN) {
                    Manager.GetSatellites()[i].Rotate(Satellite.ROTATE_FULL_TURN);
                }
            }
        }
    }

    void DoActionsOnAllRelays(int j) {
        //Manager.GetRelays().MoveVertical();
        
        int numberOfRelays = Manager.GetRelays().Count;
        for (int i = 0; i < numberOfRelays; i++) {
            Manager.GetRelays()[i].MoveVertical(); //falta hacer esta funcion en los relays
        }
    }

    void DoActionsOnAllEchos(int j) {
        Debug.Log("Echos have no actions");
    }

    public int GetState(){
        return state;
    }

    public void SetState(int aState){
        state = aState;
    }

    [System.Serializable]
    public struct ActionInfo {
        public MainLevelScript.SatTypes satTargetType;
        public ActionType actionType;       
    }


}
