using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenManager : MonoBehaviour {

    public Transform cameraTransform;


    public float animationSpeed = 1.0f;
    private float currentLerpTime;

    private IEnumerator animatorCoroutine;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}





    public IEnumerator animateOverTime(float startX, float startY, float endX, float endY, Transform targetTransform) {
        while (true) {
            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > animationSpeed) {
                currentLerpTime = animationSpeed;
            }

            float t = currentLerpTime / animationSpeed;     //No Ease
                                                            //t = Mathf.Sin(t * Mathf.PI * 0.5f);  		//Ease OUT
                                                            //t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f); 	//Ease IN
            t = t * t * (3f - 2f * t);                      //Ease IN OUT
                                                            //t = t*t*t * (t * (6f*t - 15f) + 10f);		//Ease IN OUT more

            if (t < 1.0f) {
                Vector3 newCurrentPosition;

                newCurrentPosition = new Vector3(Mathf.LerpAngle(startX, endX, t), Mathf.LerpAngle(startY, endY, t), 0.0f);
                targetTransform.position = newCurrentPosition;


                yield return 0; // wait a frame
            }
            else {
                Vector3 endPosition = new Vector3(endX, endY, 0.0f);
                targetTransform.position = endPosition; // Clamp end state;

                StopCoroutine(animatorCoroutine);
                yield return 0; // wait a frame
            }
        }
    }







}
