using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    AudioSource audioSource;

    void Start() {
        audioSource = transform.GetComponent<AudioSource>();
    } 
    
    void Update () {
        FollowCamera();
	}

    void FollowCamera() {
        transform.position = Camera.main.transform.position;
    }

    public float GetTime() {
        return audioSource.time;
    }

    
    public void SetTime(float input) {
        audioSource.time = input;
    }

    public AudioSource GetAudioSource(){
        return audioSource;
    }
    


}
