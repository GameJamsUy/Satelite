using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update () {
        FollowCamera();
	}

    void FollowCamera() {
        transform.position = Camera.main.transform.position;
    }






}
