using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {
    public enum SoundType {BUTTON_PRESS, CONNECTION_ESTABLISHED, SATELLITE_ROTATE, PLAYER_WON, PLAYER_LOST}

    public AudioClip[] shortSoundsArray;
    AudioSource shortSoundsSource;

    // Use this for initialization
    void Start () {
        shortSoundsSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        FollowCamera();
    }

    void FollowCamera() {
        transform.position = Camera.main.transform.position;
    }

    public void PlayShortSound(SoundType soundToPlay) {
        switch (soundToPlay) {
            case SoundType.BUTTON_PRESS:
                shortSoundsSource.PlayOneShot(shortSoundsArray[0]);
                break;
            case SoundType.CONNECTION_ESTABLISHED:
                shortSoundsSource.PlayOneShot(shortSoundsArray[1]);
                break;
            case SoundType.SATELLITE_ROTATE:
                shortSoundsSource.PlayOneShot(shortSoundsArray[2]);
                break;
            case SoundType.PLAYER_WON:
                shortSoundsSource.PlayOneShot(shortSoundsArray[3]);
                break;
            case SoundType.PLAYER_LOST:
                shortSoundsSource.PlayOneShot(shortSoundsArray[4]);
                break;
            default:
                break;
        }
    }

}
