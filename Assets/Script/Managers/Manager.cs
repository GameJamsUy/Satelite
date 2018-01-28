using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager{

	private static Manager manager;
	private RelayManager rManager;
    private SatelliteManager sManager;
    private EchoManager eManager;
    private CityManager cManager;
    private LeverManager lManager;
    private MusicManager mManager;

	public static Manager Inst(){
		if(Manager.manager == null){
			Manager.manager = new Manager();
		}
		return Manager.manager;
	}

    public static MusicManager MusicManager() {
        return Manager.Inst().mManager;
    }

	public static RelayManager RelayManager(){
		return Manager.Inst().rManager;
	}

    public static SatelliteManager SatelliteManager(){
        return Manager.Inst().sManager;
    }

    public static EchoManager EchoManager() {
        return Manager.Inst().eManager;
    }

    public static CityManager CityManager(){
        return Manager.Inst().cManager;
    }

    public static LeverManager LeverManager(){
        return Manager.Inst().lManager;
    }

    public static void AddMusic(Music m) {
        Manager.Inst().mManager.Add(m);
    }

    public static void AddEcho(Echo e) {
        Manager.Inst().eManager.Add(e);
    }

    public static void AddRelay(Relay r){
		Manager.Inst().rManager.Add(r);
	}

    public static void AddSatellite(Satellite s){
        Manager.Inst().sManager.Add(s);
    }
    public static void AddCity(City c){
        Manager.Inst().cManager.Add(c);
    }

    public static void AddLever(Lever l){
        Manager.Inst().lManager.Add(l);
    }



    public static AudioSource GetCurrentAudioSource(){
        return Manager.Inst().mManager.GetCurrentAudioSource();
    }

    public static void SaveMusicPlace(float input) {
        Manager.Inst().mManager.SetLengthOfMusicPlayed(input);
    }

    public static float GetMusicPlace() {
        return Manager.Inst().mManager.GetLengthOfMusicPlayed();
    }

    public static Music GetMusic() {
        return Manager.Inst().mManager.GetMusic();
    }

    public static List<Relay> GetRelays(){
		return Manager.Inst().rManager.GetRelays();
	}

    public static List<Satellite> GetSatellites(){
        return Manager.Inst().sManager.GetSatellites();
    }

    public static List<Echo> GetEchos() {
        return Manager.Inst().eManager.GetEchos();
    }
    public static List<City> GetCities(){
        return Manager.Inst().cManager.GetCities();
    }

    public static List<Lever> GetLevers(){
        return Manager.Inst().lManager.GetLevers();
    }

    public Manager(){
		if(Manager.manager != null){
			throw new UnityException("Cannot create another instance of Manager");
		}
		this.rManager = new RelayManager();
        this.sManager = new SatelliteManager();
        this.eManager = new EchoManager();
        this.cManager = new CityManager();
        this.lManager = new LeverManager();
        this.mManager = new MusicManager();
    }

	public void Destroy(){
		this.rManager.Destroy();
		this.rManager = null;

        this.sManager.Destroy();
        this.sManager = null;

        this.eManager.Destroy();
        this.eManager = null;

        this.cManager.Destroy();
        this.cManager = null;

        this.lManager.Destroy();
        this.lManager = null;
        
        this.mManager.Destroy();
        this.mManager = null;
        

		Manager.manager = null;
		//GameObject.Destroy(gameObject);
	}

    public void ResetExceptMusic(){
        this.rManager.Destroy();
        this.rManager = null;

        this.sManager.Destroy();
        this.sManager = null;

        this.eManager.Destroy();
        this.eManager = null;

        this.cManager.Destroy();
        this.cManager = null;

        this.lManager.Destroy();
        this.lManager = null;

        this.rManager = new RelayManager();
        this.sManager = new SatelliteManager();
        this.eManager = new EchoManager();
        this.cManager = new CityManager();
        this.lManager = new LeverManager();
    }
}