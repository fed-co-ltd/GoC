using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDataSaver : MonoBehaviour {
    public InputField NameField;
    public InputField PasswordField;
    public Text NameWarning;
    public Text PasswordWarning;
    public Color ActiveButtonColor;
    public Color ButtonColor;
    public Color ActivePanelColor;
    public Color PanelColor;
    bool isPlayerExist = false;
    bool isPassTheSame = false;
    string playerName;
    string playerPass;
    string kingdomTypeName;

    void Start () {
        if (SceneManager.GetActiveScene ().buildIndex == 6) {
            ReValue ();
        }
    }

    void ReValue () {
        if (GameManager.isNumPlayerChosen && GameManager.players.Count - 1 == GameManager.playerIteration) {
            NameField.GetComponentInChildren<InputField> ().text = GameManager.GetActivePlayer ().Name;
            PasswordField.GetComponentInChildren<InputField> ().text = GameManager.GetActivePlayer ().Password;
            ChangeName ();
            ChangePass ();
        }
    }
    public void ChangeName () {
        playerName = NameField.GetComponentInChildren<InputField> ().text;
        if (CheckIfNameExist ()) {
            NameWarning.text = "Username already taken. play as this user?";
            PasswordWarning.text = "Enter the password for that username above";
            isPlayerExist = true;
        } else {
            NameWarning.text = "Enter your name above";
            PasswordWarning.text = "Create a password for that username above";
            isPlayerExist = false;
        }
    }

    public void ChangePass () {
        playerPass = PasswordField.GetComponentInChildren<InputField> ().text;
        if (isPlayerExist) {
            if (CheckIfPassIsTheSame ()) {
                isPassTheSame = true;
                PasswordWarning.text = "Correct password for that username above";
            } else {
                isPassTheSame = false;
                PasswordWarning.text = "Wrong password for that username above";
            }

        }
    }

    bool CheckIfNameExist () {
        var db = "GoC.sqlite";
        var nameChecker = new DataSaver ().DataBase (db);
        var command = String.Format ("* FROM PlayersData WHERE Name='{0}'", playerName);
        var command1 = "PlayersData (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Name TEXT NOT NULL,Password TEXT NOT NULL, `Date` TEXT NOT NULL, `LatestDate` TEXT,Kingdom TEXT, Score INTEGER DEFAULT 0);";

        nameChecker.Open ();
        nameChecker.CreateTable (command1);
        var reader = nameChecker.Select (command);
        if (reader.Read ()) {
            nameChecker.Close ();
            return true;
        }
        nameChecker.Close ();
        return false;
    }

    bool CheckIfPassIsTheSame () {
        var db = "GoC.sqlite";
        var passChecker = new DataSaver ().DataBase (db);
        var command = String.Format ("* FROM PlayersData WHERE Name='{0}'", playerName);
        var command1 = "PlayersData (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Name TEXT NOT NULL,Password TEXT NOT NULL, `Date` TEXT NOT NULL, `LatestDate` TEXT,Kingdom TEXT, Score INTEGER DEFAULT 0);";

        passChecker.Open ();
        passChecker.CreateTable (command1);
        var reader = passChecker.Select (command);
        if (reader.Read ()) {
            if (reader.GetString (2) == playerPass) {
                passChecker.Close ();
                return true;
            }
        }
        passChecker.Close ();
        return false;
    }
    public void StorePData () {
        if (playerName != null && playerPass != null) {
            if (isPlayerExist) {
                if (isPassTheSame) {
                    GameManager.SavePlayerData (playerName, playerPass, true);
                }
            } else {
                GameManager.SavePlayerData (playerName, playerPass);
            }
        }
    }
    public void ChangeNumPlayers (int num) {
        GameManager.numPlayers = num;
        GameManager.isNumPlayerChosen = true;
        for (int i = 2; i < 5; i++) {
            var buttonName = i + " Players Button";
            var button = GameObject.Find (buttonName).GetComponentInChildren<Button> ();
            ColorBlock cb = button.colors;
            cb.normalColor = ButtonColor;
            button.colors = cb;
            if (i == num) {
                cb.normalColor = ActiveButtonColor;
                cb.highlightedColor = ActiveButtonColor;
                button.colors = cb;
            }
        }
    }

    public void ChangeKingdom (int num) {

        for (int i = 1; i < 5; i++) {
            var buttonName = "Kingdom (" + i + ")";
            var button = GameObject.Find (buttonName).GetComponentInChildren<Button> ();
            var panel = button.transform.GetChild (0).GetComponentInChildren<Image> ();
            panel.color = PanelColor;
            if (i == num) {
                panel.color = ActivePanelColor;
            }
        }
        switch (num) {
            case 1:
                kingdomTypeName = "Q";
                break;
            case 2:
                kingdomTypeName = "W";
                break;
            case 3:
                kingdomTypeName = "E";
                break;
            case 4:
                kingdomTypeName = "R";
                break;
            default:
                kingdomTypeName = "default";
                break;
        }
    }
    public void StorePDataKingdom () {
        GameManager.ChangeKingdom (kingdomTypeName);
    }

    public void CheckLoadScene (int num) {
        bool condition = GameManager.nextPlayerIteratation ();
        if (condition) {
            SceneManager.LoadScene (6);
        } else {
            SceneManager.LoadScene (num);
        }
    }

}