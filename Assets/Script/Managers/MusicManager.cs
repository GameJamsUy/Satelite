using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MusicManager {

    private Music music;
    private int length;
    private float lengthOfSongPlayed = 0.0f;


    public void SetLengthOfMusicPlayed(float input) {
        lengthOfSongPlayed = input;        
    }

    public float GetLengthOfMusicPlayed() {
        return lengthOfSongPlayed;
    }

    public AudioSource GetCurrentAudioSource(){
        return music.GetAudioSource();
    }

    public MusicManager() {
        music = null;
        length = 0;
    }

    public int GetLength() {
        return this.length;
    }

    public Music GetMusic() {
        return this.music;
    }

    public void Add(Music aMusic) {
        music = aMusic;
    }

    public void Destroy() {
        music = null;
        this.length = 0;
    }
}