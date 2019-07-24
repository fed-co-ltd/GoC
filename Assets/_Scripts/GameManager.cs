using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private static string SavePath_Settings;
    void Awake () {
        SavePath_Settings = Application.persistentDataPath + "/settings_data.save";
        if (instance != null) {
            Destroy (gameObject);
        } else {
            instance = this;
            GameObject.DontDestroyOnLoad (gameObject);
        }
    }

    static void SaveData (string savePath, object saved) {
       
            var binaryFormatter = new BinaryFormatter ();
            using (var fileStream = File.Create (savePath)) {
                binaryFormatter.Serialize (fileStream, saved);
            }

    }

    public static void SaveSettingsData () {
        var save = new SettingsData ();
        save.SetVolumesData (SettingsManager.MasterVolume,
            SettingsManager.MusicVolume,
            SettingsManager.SoundFXVolume);
        SaveData (SavePath_Settings, save);
    }

        
    public static void LoadSettingsData () {
        SettingsData save = new SettingsData ();
        if (File.Exists (SavePath_Settings)) {
            var binaryFormatter = new BinaryFormatter ();
            using (var fileStream = File.Open (SavePath_Settings, FileMode.Open)) {
                save = (SettingsData) binaryFormatter.Deserialize (fileStream);
            }
        } else {
            Debug.LogWarning ("Save file doesn't exist.");
        }
    
        SettingsManager.MasterVolume = save.MasterVolume;
        SettingsManager.MusicVolume = save.MusicVolume;
        SettingsManager.SoundFXVolume = save.SoundFXVolume;
        SettingsManager.UpdateVolume ();

    }

}