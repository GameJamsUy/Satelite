using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitatingBehaviour : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedZ;
    public bool oscillate;
    public float maxY;
    public float minY;
    public bool upward;

    private float timer;
    private bool goingUp;
    public float vel = -10;
    public float accel = 5;

    private Vector2 startPos;

    void Start(){
        startPos = transform.position;
        maxY = startPos.y + maxY;
        minY = startPos.y - minY;        
    }

    void Update()
    {
        if (oscillate)
        {
            vel += accel * Time.deltaTime;

            if (transform.position.y <= maxY && upward){
                accel = -accel;
                upward = !upward;
            }

            else if(transform.position.y >= minY && !upward){
                accel = -accel;
                upward = !upward;
            }

            transform.position = new Vector2(transform.position.x, transform.position.y + vel * Time.deltaTime);
            transform.Rotate(new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ * Time.deltaTime));
        }

        else
        {
            transform.Rotate(new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ * Time.deltaTime));
        }
    }
}
