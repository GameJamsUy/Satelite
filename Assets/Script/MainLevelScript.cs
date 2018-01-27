using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelScript : MonoBehaviour {

    public GameObject satellitePrefab;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 5; i++){
            GameObject go = Instantiate(satellitePrefab);
            go.transform.position = new Vector2(-200 + (100*i), 0);
            Satellite satellite = go.GetComponent<Satellite>();
            //
        }
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}
}
