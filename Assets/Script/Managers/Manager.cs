using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager{

	private static Manager manager;
	private RelayManager rManager;
    private SatelliteManager sManager;

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

    public Manager(){
		if(Manager.manager != null){
			throw new UnityException("Cannot create another instance of Manager");
		}
		this.rManager = new RelayManager();
	}

	public void Destroy(){
		this.rManager.Destroy();
		this.rManager = null;

        this.sManager.Destroy();
        this.sManager = null;

		Manager.manager = null;
		//GameObject.Destroy(gameObject);
	}
}