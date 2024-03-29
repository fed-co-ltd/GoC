﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {
    public static float MasterVolume = 0.5f;
    public static float MusicVolume = 1f;
    public static float SoundFXVolume = 1f;

    void Start () {
        GameManager.LoadSettingsData();
    }
    public static void UpdateVolume () {
        if (GameManager.instance != null) {
            var musicPLayer = GameObject.Find ("*Music Player");
            var audio = musicPLayer.GetComponentInChildren<AudioSource> ();
            audio.volume = MasterVolume * MusicVolume;
        }
        GameManager.SaveSettingsData();

    }
    public static void ChangeMusicVolume (float size) {
        MusicVolume = (size/100);
        UpdateVolume ();
    }

    public static void ChangeMasterVolume (float size) {
        MasterVolume = (size/100);
        UpdateVolume ();
    }

    public static void ChangeSoundsVolume (float size) {
        SoundFXVolume = (size/100);
    }
}