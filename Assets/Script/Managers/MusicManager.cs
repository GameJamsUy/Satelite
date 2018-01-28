using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MusicManager {

    private List<Music> musics;
    private int length;
    private float lengthOfSongPlayed = 0.0f;


    public void SaveLengthOfMusicPlayed(float input) {
        lengthOfSongPlayed = input;
    }

    public float GetLengthOfMusicPlayed() {
        return lengthOfSongPlayed;
    }

    public MusicManager() {
        musics = new List<Music>();
        length = 0;
    }

    public int GetLength() {
        return this.length;
    }

    public List<Music> GetMusics() {
        return this.musics;
    }

    public void Add(Music music) {
        if (this.musics.Contains(music)) {
            throw new ArgumentException("The music is already contained in the list.");
        }
        this.musics.Add(music);
        this.length = this.length + 1;
    }

    private void RemoveAt(int index) {
        if (index < 0 || index >= length) {
            throw new ArgumentException("Invalid index: " + index);
        }
        this.musics.RemoveAt(index);
        this.length = this.length - 1;
    }

    private void Remove(Music music) {
        if (!this.musics.Contains(music)) {
            throw new ArgumentException("Music not found");
        }
        this.musics.Remove(music);
    }

    public void Destroy() {
        foreach (Music currMusic in this.musics) {
            if (currMusic != null) {
                //currCity.Destroy();
            }
        }
        this.musics.Clear();
        this.musics = new List<Music>();
        this.length = 0;
    }
}