using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RelayManager{

	private List<Relay> relays;
	private int length;

	public RelayManager(){
		relays = new List<Relay>();
		length = 0;
	}

	public int GetLength(){
		return this.length;
	}

	public List<Relay> GetRelays(){
		return this.relays;
	}

	public void Add(Relay relay){
		if(this.relays.Contains(relay)){
			throw new ArgumentException("The relay is already contained in the list.");
		}
		this.relays.Add(relay);
		this.length = this.length + 1;
	}

	private void RemoveAt(int index){
		if(index < 0 || index >= length){
			throw new ArgumentException("Invalid index: " + index);
		}
		this.relays.RemoveAt(index);
		this.length = this.length - 1;
	}

	private void Remove(Relay relay){
		if(!this.relays.Contains(relay)){
			throw new ArgumentException("Relay not found");
		}
		this.relays.Remove(relay);
	}
		
	public void Destroy(){
		foreach(Relay currRelay in this.relays){
			if (currRelay != null) {
				//currRelay.Destroy();
			}
		}
		this.relays.Clear();
		this.relays = new List<Relay>();
		this.length = 0;
	}
}