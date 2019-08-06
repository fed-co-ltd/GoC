using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoC;

public class PlayerDataSaver : MonoBehaviour {
    public InputField NameField;
    public InputField PasswordField;
    public Text NameWarning;
    public Text PasswordWarning;
    public Text ConfirmTitle;
    public Text ConfirmText;
    public Color ActiveButtonColor;
    public Color ButtonColor;
    public Color ActivePanelColor;
    public Color PanelColor;
    bool isPlayerExist = false;
    bool hasError = false;
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
            GameManager.players.RemoveLast();
            ChangeName ();
            ChangePass ();
        }
    }
    public void ChangeName () {
        playerName = NameField.GetComponentInChildren<InputField> ().text;
        
        if (!GameManager.players.IsPlayerExist (playerName)) {
            PasswordField.interactable = true;
            if (CheckIfNameExist ()) {
                NameWarning.text = "Username already taken. play as this user?";
                GameManager.ChangeLog ("LOG: Username already taken.");
                PasswordWarning.text = "Enter the password for that username above";
                isPlayerExist = true;
            } else {
                NameWarning.text = "Enter your name above";
                GameManager.ChangeLog ("LOG: New player");
                PasswordWarning.text = "Create a password for that username above";
                isPlayerExist = false;
            }
        }else{
            NameWarning.text = "Playername already taken by another playing player ";
            PasswordWarning.text = "Password is disabled";
            PasswordField.interactable = false;
        }
    }

    public void ChangePass () {
        playerPass = PasswordField.GetComponentInChildren<InputField> ().text;

        if (!GameManager.players.IsPlayerExist (playerName)) {
            if (isPlayerExist) {
                if (CheckIfPassIsTheSame ()) {
                    hasError = false;
                    PasswordWarning.text = "Correct password for that username above";
                    GameManager.ChangeLog ("LOG: Correct pass");
                } else {
                    hasError = true;
                    if (playerPass == "" || playerName == "") {
                        hasError = false;
                    }
                    PasswordWarning.text = "Wrong password for that username above";
                    ConfirmTitle.text = "WRONG PASSWORD";
                    ConfirmText.text = "Please input the password,appropriate for the playername you entered, If so happens that you forgot your password. \n\n Email us at: fed_company@gmail.com";
                    GameManager.ChangeLog ("LOG: Wrong pass.");
                }

            } else {
                hasError = false;
                GameManager.ChangeLog ("LOG: Change pass successful");
            }
        } else {
            ConfirmTitle.text = "SIMILAR NAME";
            ConfirmText.text = "The player name you entered is already taken by another player";
            ConfirmText.text += "\nPlease Enter another name";
            hasError = true;
        }
    }

    bool CheckIfNameExist () {
        var db = "GoC.db";
        var nameChecker = new DataSaver ().DataBase (db);
        var command = String.Format ("* FROM PlayersData WHERE Name='{0}'", playerName);
        var command1 = "`PlayersData` (`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,`Name`	TEXT NOT NULL,`Password`	TEXT NOT NULL,`Date`	TEXT NOT NULL,`LatestDate`	TEXT,`Kingdom`	TEXT,`HighestScore`	INTEGER DEFAULT 0,`LatestScore`	INTEGER DEFAULT 0);";

        nameChecker.Open ();
        GameManager.ChangeLog ("LOG: Database opened");
        nameChecker.CreateTable (command1);
        GameManager.ChangeLog ("LOG: Table created");
        var reader = nameChecker.Select (command);
        if (reader.Read ()) {
            nameChecker.Close ();
            return true;
        }
        nameChecker.Close ();
        return false;
    }

    bool CheckIfPassIsTheSame () {
        var db = "GoC.db";
        var passChecker = new DataSaver ().DataBase (db);
        var command = String.Format ("* FROM PlayersData WHERE Name='{0}'", playerName);
        var command1 = "`PlayersData` (`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,`Name`	TEXT NOT NULL,`Password`	TEXT NOT NULL,`Date`	TEXT NOT NULL,`LatestDate`	TEXT,`Kingdom`	TEXT,`HighestScore`	INTEGER DEFAULT 0,`LatestScore`	INTEGER DEFAULT 0);";

        passChecker.Open ();
        passChecker.CreateTable (command1);
        var reader = passChecker.Select (command);
        if (reader.Read ()) {
            using (SHA256 sha256Hash = SHA256.Create ()) {
                if (Hasher.VerifyHash (sha256Hash, playerPass, reader.GetString (2))) {
                    passChecker.Close ();
                    return true;
                }
            }
        }
        passChecker.Close ();
        return false;
    }
    public void ConfirmPlayerData () {
        ChangeName ();
        ChangePass ();
        var panel = GameObject.Find ("Next Button").GetComponent<OpenPanel> ();
        if (playerName != "" && playerPass != "" && !hasError) {
            StorePData ();
        } else {
            if (!hasError) {
                ConfirmTitle.text = "REQUIRED";
                ConfirmText.text = "";
                if (playerName == "") {
                    ConfirmText.text += "Player name is a required field\n";
                }
                if (playerPass == "") {
                    ConfirmText.text += "Password is a required field";
                }
                GameManager.ChangeLog ("LOG: SavePlayerData not executed, null name and pass");
            }

            panel.Panel (true);
        }
    }
    public void ClearPlayersData(){
        GameManager.players.RemoveAll();
        GameManager.numPlayers = 2;
        GameManager.playerIteration = 0;
    }
    public void StorePData () {
        if (isPlayerExist) {
            GameManager.SavePlayerData (playerName, playerPass, true);
            GameManager.ChangeLog ("LOG: SavePlayerData Executed");
        } else {
            GameManager.SavePlayerData (playerName, playerPass);
            GameManager.ChangeLog ("LOG: SavePlayerData Executed.new player");
        }
        var manager = GameObject.Find ("*Scenes Manager").GetComponent<ScenesManager> ();
        manager.LoadScene (7);
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
                cb.normalColor = button.colors.selectedColor;
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
        kingdomTypeName = ((Kingdom) num).ToString();
        GameManager.GetActivePlayer().Kingdom = num;
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