using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelScript : MonoBehaviour {
    public enum SatTypes {COLOR_RED, COLOR_GREEN, COLOR_BLUE, RELAY, ECHO};
    public enum SatRotation {UP, DOWN, RIGHT, LEFT};

    //public SatelliteTypes[] satsToSpawn;

    public GameObject satellitePrefab;
    public GameObject relayPrefab;
    public GameObject echoPrefab;

    public SatInfo[] satsToSpawn;

    void Awake(){
        Manager.Inst().Destroy();
    }
    // Use this for initialization
    void Start () {
        for (int i = 0; i <= GameConstants.maxX; i++){
            switch (satsToSpawn[i].satType) {
                case SatTypes.COLOR_RED:
                    SpawnSatellite(i);
                    break;
                case SatTypes.COLOR_GREEN:
                    SpawnSatellite(i);
                    break;
                case SatTypes.COLOR_BLUE:
                    SpawnSatellite(i);
                    break;
                case SatTypes.RELAY:
                    SpawnRelay(i);
                    break;
                case SatTypes.ECHO:
                    SpawnEcho(i);
                    break;
                default:
                    break;
            }
            //SpawnSatellite(i);
        }
	}

    void SpawnSatellite(int i) {
        GameObject go = Instantiate(satellitePrefab);
        Satellite satellite = go.GetComponent<Satellite>();
      
        // sets satellite color
        if (satsToSpawn[i].satType == SatTypes.COLOR_RED) {
            satellite.SetColor(Satellite.COLOR_RED);
        }
        else if (satsToSpawn[i].satType == SatTypes.COLOR_GREEN) {
            satellite.SetColor(Satellite.COLOR_GREEN);
        }
        else if (satsToSpawn[i].satType == SatTypes.COLOR_BLUE) {
            satellite.SetColor(Satellite.COLOR_BLUE);
        }

        // sets satellite starting rotation 
        if (satsToSpawn[i].satRotation == SatRotation.UP) {
            satellite.SetStartingRotation(Satellite.ANGLE_UP);
        }
        else if (satsToSpawn[i].satRotation == SatRotation.DOWN) {
            satellite.SetStartingRotation(Satellite.ANGLE_DOWN);
        }
        else if (satsToSpawn[i].satRotation == SatRotation.LEFT) {
            //satellite.SetStartingRotation(Satellite.ANGLE_LEFT);
            satellite.SetStartingRotation(Satellite.ANGLE_RIGHT);

        }
        else if (satsToSpawn[i].satRotation == SatRotation.RIGHT) {
            //satellite.SetStartingRotation(Satellite.ANGLE_RIGHT);
            satellite.SetStartingRotation(Satellite.ANGLE_LEFT);
        }

        satellite.SetX(i);
        satellite.SetY(satsToSpawn[i].satRow);

        Manager.AddSatellite(satellite);
    }

    
    void SpawnRelay(int i) {
        GameObject go = Instantiate(relayPrefab);
        Relay relay = go.GetComponent<Relay>();
        relay.SetX(i);
        relay.SetY(satsToSpawn[i].satRow);
        Manager.AddRelay(relay);
    }


    void SpawnEcho(int i) {
        GameObject go = Instantiate(echoPrefab);
        Echo echo = go.GetComponent<Echo>();
        echo.SetX(i);
        echo.SetY(satsToSpawn[i].satRow);
        Manager.AddEcho(echo);
    }

    void OnDestroy(){
        Manager.Inst().Destroy();
    }

    [System.Serializable]
    public struct SatInfo {
        public SatTypes satType;
        public int satRow;
        public SatRotation satRotation;
    }


    
}
