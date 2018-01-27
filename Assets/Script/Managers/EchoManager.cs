using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EchoManager{

	private List<Echo> echos;
	private int length;

	public EchoManager(){
		echos = new List<Echo>();
		length = 0;
	}

	public int GetLength(){
		return this.length;
	}

	public List<Echo> GetEchos(){
		return this.echos;
	}

	public void Add(Echo echo){
		if(this.echos.Contains(echo)){
			throw new ArgumentException("The echo is already contained in the list.");
		}
		this.echos.Add(echo);
		this.length = this.length + 1;
	}

	private void RemoveAt(int index){
		if(index < 0 || index >= length){
			throw new ArgumentException("Invalid index: " + index);
		}
		this.echos.RemoveAt(index);
		this.length = this.length - 1;
	}

	private void Remove(Echo echo) {
		if(!this.echos.Contains(echo)){
			throw new ArgumentException("Echo not found");
		}
		this.echos.Remove(echo);
	}
		
	public void Destroy(){
		foreach(Echo currEcho in this.echos){
			if (currEcho != null) {
				//currEcho.Destroy();
			}
		}
		this.echos.Clear();
		this.echos = new List<Echo>();
		this.length = 0;
	}
}