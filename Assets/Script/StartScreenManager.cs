using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour {
    public Transform cameraTransform;
    public Transform creditsScreen;
    public Transform mainMenuScreen;

    public GameObject soundPrefab;
    public float animationSpeed = 1.0f;
    private float currentLerpTime;
    private IEnumerator animatorCoroutine;

    private SoundScript soundScript;



    // Use this for initialization
    void Start () {
        SpawnSoundPlayer();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
        
    public void CreditsClick() {
        soundScript.PlayShortSound(SoundScript.SoundType.BUTTON_PRESS);
        currentLerpTime = 0f;
        animatorCoroutine = animateOverTime(cameraTransform.position.x, creditsScreen.position.x, cameraTransform.position.y, cameraTransform.position.z, cameraTransform);
        StartCoroutine(animatorCoroutine);
    }

    public void BackCreditsClick() {
        soundScript.PlayShortSound(SoundScript.SoundType.BUTTON_PRESS);
        currentLerpTime = 0f;
        animatorCoroutine = animateOverTime(cameraTransform.position.x, mainMenuScreen.position.x, cameraTransform.position.y, cameraTransform.position.z, cameraTransform);
        StartCoroutine(animatorCoroutine);
    }

    public void PlayButtonClick() {
        //soundScript.PlayShortSound(SoundScript.SoundType.BUTTON_PRESS);
        SceneSwitcher sceneSwitcher = transform.GetComponent<SceneSwitcher>();
        sceneSwitcher.LoadNextScene();
    }


    void SpawnSoundPlayer() {        
        GameObject soundPlayerInstance = Instantiate(soundPrefab) as GameObject;
        soundPlayerInstance.transform.SetParent(transform.root);
        soundScript = soundPlayerInstance.GetComponent<SoundScript>();
    }

    public IEnumerator animateOverTime(float startX, float endX, float startY, float startZ, Transform targetTransform) {
        while (true) {
            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > animationSpeed) {
                currentLerpTime = animationSpeed;
            }

            float t = currentLerpTime / animationSpeed;     //No Ease
                                                            //t = Mathf.Sin(t * Mathf.PI * 0.5f);  		//Ease OUT
                                                            //t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f); 	//Ease IN
            t = t * t * (3f - 2f * t);                                                                  //Ease IN OUT
                                                            //t = t*t*t * (t * (6f*t - 15f) + 10f);		//Ease IN OUT more

            if (t < 1.0f) {
                Vector3 newCurrentPosition;

                newCurrentPosition = new Vector3(Mathf.Lerp(startX, endX, t), startY, startZ);
                targetTransform.position = newCurrentPosition;


                yield return 0; // wait a frame
            }
            else {
                Vector3 endPosition = new Vector3(endX, startY, startZ);
                targetTransform.position = endPosition; // Clamp end state;

                StopCoroutine(animatorCoroutine);
                yield return 0; // wait a frame
            }
        }
    }







}
