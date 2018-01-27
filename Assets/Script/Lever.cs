using UnityEngine;

public class Lever : MonoBehaviour {

    public enum ActionType {ROTATE_RIGHT, ROTATE_LEFT, ROTATE_INVERT, ROTATE_FULL_TURN, MOVE_VERTICAL, NO_ACTION}

    public ActionInfo[] leverActions;

    private Animator animator;

    private void Awake() {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }


    int red = 0;
    int green = 1;
    int blue = 2;

    public void LeverGetsClicked() {
        for (int i = 0; i < leverActions.Length; i++) {
            PlayButtonAnimation();
            switch (leverActions[i].satTargetType) {
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
    }

    void PlayButtonAnimation() {
        animator.SetTrigger("buttonPress");
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

    [System.Serializable]
    public struct ActionInfo {
        public MainLevelScript.SatTypes satTargetType;
        public ActionType actionType;       
    }


}
