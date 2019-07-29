using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoC;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    static SettingsData settingsData;
    static Serializer settingsSerializer;
    public static Players players = new Players ();
    public static int numPlayers = 2;
    public static int playerIteration = 0;
    public static bool isNumPlayerChosen = false;

    void Awake () {
        Serializer.SavePathDirectory = Application.persistentDataPath + "/saves/";

        if (instance != null) {
            Destroy (gameObject);
        } else {
            instance = this;
            GameObject.DontDestroyOnLoad (gameObject);
        }
        settingsData = new SettingsData ();
        CreateSerializers ();
    }

    public static bool nextPlayerIteratation () {

        if (players.Count < numPlayers) {
            ++playerIteration;
            return true;
        }
        return false;
    }

    public static PlayerData GetActivePlayer () {
        return players[playerIteration];
    }
    void CreateSerializers () {
        settingsSerializer = new Serializer () {
            saveFileName = "settings_data.save",
            dataObject = settingsData
        };
    }

    public static void SaveSettingsData () {
        settingsData.SetVolumesData (SettingsManager.MasterVolume,
            SettingsManager.MusicVolume,
            SettingsManager.SoundFXVolume);
        settingsSerializer.SaveData (settingsData);
    }

    public static void LoadSettingsData () {
        settingsSerializer.Initialize ();
        settingsData = (SettingsData) settingsSerializer.LoadData (settingsData);
        SettingsManager.MasterVolume = settingsData.MasterVolume;
        SettingsManager.MusicVolume = settingsData.MusicVolume;
        SettingsManager.SoundFXVolume = settingsData.SoundFXVolume;
        SettingsManager.UpdateVolume ();
    }
    public static void SavePlayerData (string name, string pass, bool isUpdate = false) {
        DateTime dateNow = DateTime.UtcNow;
        PlayerData data = new PlayerData ();
        data.Store (name, pass, dateNow);
        players.Add (data);

        var db = "GoC.sqlite";
        var playerSaver = new DataSaver ().DataBase (db);

        var command1 = "PlayersData (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Name TEXT NOT NULL,Password TEXT NOT NULL, `Date` TEXT NOT NULL, `LatestDate` TEXT,Kingdom TEXT, Score INTEGER DEFAULT 0);";
        var command2 = String.Format ("PlayersData (Name, Password, Date, LatestDate, Kingdom) VALUES ('{0}', '{1}', '{2}', '{3}', 'Default');", data.Name, data.Password, data.playDate.ToString (), data.playDate.ToString ());
        if (isUpdate) {
            command2 = String.Format ("PlayersData SET LatestDate = '{0}' WHERE Name='{1}';", DateTime.UtcNow.ToString (), name);
        }

        playerSaver.Open ();
        playerSaver.CreateTable (command1);
        if (isUpdate) {
            playerSaver.Update (command2);
        } else {
            playerSaver.Insert (command2);
        }
        playerSaver.Close ();
    }

    public static void ChangeKingdom (string kingdom) {
        var db = "GoC.sqlite";
        var playerSaver = new DataSaver ().DataBase (db);
        var name = GameManager.GetActivePlayer ().Name;
        playerSaver.Open ();
        var command = "PlayersData SET Kingdom='" + kingdom + "' WHERE Name='" + name + "';";
        playerSaver.Update (command);
        playerSaver.Close ();
    }
}