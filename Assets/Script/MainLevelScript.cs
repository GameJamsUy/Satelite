using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelScript : MonoBehaviour {
    public enum SatTypes {COLOR_RED, COLOR_GREEN, COLOR_BLUE, RELAY, ECHO, NONE};
    public enum SatRotation {UP, DOWN, RIGHT, LEFT};
    public int maxMovementsAllowed;

    //public SatelliteTypes[] satsToSpawn;

    public GameObject satellitePrefab;
    public GameObject relayPrefab;
    public GameObject echoPrefab;
    public GameObject cityPrefab;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject soundPrefab;
    public int turnsForThreeStars;
    public int turnsForTwoStars;
    private int stars;

    public bool playerInputEnabled;
    public bool[] citiesInfo;
    public SatInfo[] satsToSpawn;
    private bool won = false;
    private bool lost = false;
    private int currentMovements;
    private int actionsLeft;

    private SoundScript soundScript;

    private float waitAfterEndGameTime = 3.0f;

    void Awake(){
        Manager.Inst().Destroy();
    }
    // Use this for initialization
    void Start () {
        playerInputEnabled = true;
        SpawnSoundPlayer();
        SpawnCity();
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
        }

        SetActionsLeft(maxMovementsAllowed);
        SetCurrentMovements(0);      
	}

    void Update(){
        if (CheckWinCondition() && !won){
            won = true;
            StartCoroutine(EndOfGameEvent(true));
        }
        else if (lost){
            StartCoroutine(EndOfGameEvent(false));
        }
    }

    IEnumerator EndOfGameEvent(bool state) {
        playerInputEnabled = false;
        yield return new WaitForSeconds(waitAfterEndGameTime);
        if (state) {
            WonActions();
        }
        else {
            LostActions();
        }
    }


    void WonActions() {      
        GameObject go = Instantiate(winScreen);
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (currentMovements <= turnsForThreeStars) {
            stars = 3;
        }
        else if (currentMovements > turnsForThreeStars && actionsLeft <= turnsForTwoStars) {
            stars = 2;
        }
        else {
            stars = 1;
        }
        //Debug.Log("stars: " + stars);
        for (int i = 0; i < stars; i++) {
            go.GetComponent<WonScreen>().stars[i].SetActive(true);
        }
    }

    void LostActions() {
        GameObject go = Instantiate(loseScreen);
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        lost = false; // esto es asi?
    }


    void SpawnCity() {
        for (int i = 0; i < citiesInfo.Length; i++) {
            if (citiesInfo[i] == true) {
                GameObject go = Instantiate(cityPrefab);
                City city = go.GetComponent<City>();
                city.SetX(i);
                city.SetY(0);
                Manager.AddCity(city);
            }
        }
    }

    void SpawnSoundPlayer() {
        GameObject soundPlayerInstance = Instantiate(soundPrefab) as GameObject;
        soundPlayerInstance.transform.SetParent(transform.root);
        soundScript = soundPlayerInstance.GetComponent<SoundScript>();
        transform.root.GetComponent<SceneSwitcher>().soundRef = soundScript;
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
            satellite.SetStartingRotation(Satellite.ANGLE_LEFT);

        }
        else if (satsToSpawn[i].satRotation == SatRotation.RIGHT) {
            satellite.SetStartingRotation(Satellite.ANGLE_RIGHT);
        }

        satellite.SetX(i);
        satellite.SetY(satsToSpawn[i].satRow);
        satellite.transform.SetParent(transform.root);

        Manager.AddSatellite(satellite);
    }

    bool CheckWinCondition(){
        foreach (City currCity in Manager.GetCities()){
            if(currCity.GetState() == City.STATE_OFF){
                return false;
            }
        }
        if (Manager.GetCities().Count == 0){
            return false;
        }
        return true;
    }


    void SetLoseCondition(bool value){
        lost = true;
    }
    
    void SpawnRelay(int i) {
        GameObject go = Instantiate(relayPrefab);
        Relay relay = go.GetComponent<Relay>();
        relay.SetX(i);
        relay.SetY(satsToSpawn[i].satRow);
        relay.transform.SetParent(transform.root);
        Manager.AddRelay(relay);
    }


    void SpawnEcho(int i) {
        GameObject go = Instantiate(echoPrefab);
        Echo echo = go.GetComponent<Echo>();
        echo.SetX(i);
        echo.SetY(satsToSpawn[i].satRow);
        echo.transform.SetParent(transform.root);
        Manager.AddEcho(echo);
    }

    public int GetCurrentMovements(){
        return currentMovements;
    }

    public void SetCurrentMovements(int aValue){
        currentMovements = aValue;
    }

    public int GetActionsLeft(){
        return actionsLeft;
    }

    public void SetActionsLeft(int aValue){
        actionsLeft = aValue;
        if(actionsLeft <= 0){
            SetLoseCondition(true);
        }
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
