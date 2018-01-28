using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxDrift : MonoBehaviour {
    public float speedX;
    public float speedY;
    public float minX;
    public float maxX;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        transform.position = new Vector2(transform.position.x + speedX * Time.deltaTime, transform.position.y);
        if(transform.position.x <= minX){
            speedX = -speedX;
            transform.position = new Vector2(minX + 3, transform.position.y);
        }
        if (transform.position.x >= maxX){
            speedX = -speedX;
            transform.position = new Vector2(maxX - 3, transform.position.y);
        }
    }
}
