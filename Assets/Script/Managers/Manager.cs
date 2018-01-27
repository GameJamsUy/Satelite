using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager{

	private static Manager manager;
	private RelayManager rManager;
    private SatelliteManager sManager;
    private EchoManager eManager;

	public static Manager Inst(){
		if(Manager.manager == null){
			Manager.manager = new Manager();
		}
		return Manager.manager;
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

    public static void AddEcho(Echo e) {
        Manager.Inst().eManager.Add(e);
    }

    public static void AddRelay(Relay r){
		Manager.Inst().rManager.Add(r);
	}

    public static void AddSatellite(Satellite s){
        Manager.Inst().sManager.Add(s);
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

    public Manager(){
		if(Manager.manager != null){
			throw new UnityException("Cannot create another instance of Manager");
		}
		this.rManager = new RelayManager();
        this.sManager = new SatelliteManager();
        this.eManager = new EchoManager();
    }

	public void Destroy(){
		this.rManager.Destroy();
		this.rManager = null;

        this.sManager.Destroy();
        this.sManager = null;

        this.eManager.Destroy();
        this.eManager = null;

		Manager.manager = null;
		//GameObject.Destroy(gameObject);
	}
}