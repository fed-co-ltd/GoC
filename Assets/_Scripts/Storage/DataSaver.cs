using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.AccessControl;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataSaver{
    private string ConnectionString;
    private string SavePathDirectory;
    private string DBfileName;
    private IDbConnection  DBconnection;
    private string DBcommandText;
    public DataSaver () {
        SavePathDirectory = Application.persistentDataPath + "/saves/";
        if (!Directory.Exists (SavePathDirectory)) {
            Directory.CreateDirectory (SavePathDirectory);
        }
    }

    public DataSaver DataBase (string fileName) {
        DBfileName = fileName;
        return this;
    }
    public DataSaver Open () {
        ConnectionString = "URI=file:" + SavePathDirectory + DBfileName;
        if (!File.Exists (SavePathDirectory + DBfileName)) {
            CreateDataBase (DBfileName);
        }
        DBconnection = new SqliteConnection (ConnectionString);
        DBconnection.Open ();
        return this;
    }

    public void CreateDataBase (string fileName) {
        /* FileStream file = new FileStream(fileName,
                                         FileMode.Create,
                                         FileAccess.ReadWrite,
                                         FileShare.ReadWrite,
                                         4096,
                                         true
        );
        file.Close();*/
        //StartCoroutine(StartTimer());
        WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/saves/" + fileName);
        Debug.Log("to load ...");
        while (!loadDB.isDone) {
            Debug.Log("loading ...");
         }
         
        // then save to Application.persistentDataPath
        File.WriteAllBytes(SavePathDirectory + DBfileName, loadDB.bytes);
    }

    int TimerSpan(DateTime start){
        TimeSpan span = DateTime.Now - start;
        return span.Seconds;
    }

    public void Close () {
        DBconnection.Close ();
    }

    public IDataReader Select (string command) {
        using (IDbCommand cmd = DBconnection.CreateCommand ()) {
            DBcommandText = "SELECT " + command;
            cmd.CommandText = DBcommandText;
            var reader = cmd.ExecuteReader ();
            return reader;            
        }

    }

    public bool Insert (string command) {
        using (IDbCommand cmd = DBconnection.CreateCommand ()) {
            DBcommandText = "INSERT INTO " + command;
            cmd.CommandText = DBcommandText;
            if (cmd.ExecuteNonQuery () > 0) {
                return true;
            }
            return false;
        }
    }

    public bool UpdateData (string command) {
        using (IDbCommand cmd = DBconnection.CreateCommand ()) {
            DBcommandText = "UPDATE " + command;
            cmd.CommandText = DBcommandText;
            if (cmd.ExecuteNonQuery () > 0) {
                return true;
            }
            return false;
        }
    }

    public void CreateTable (string command) {
        using (IDbCommand cmd = DBconnection.CreateCommand ()) {
            DBcommandText = "CREATE TABLE IF NOT EXISTS " + command;
            cmd.CommandText = DBcommandText;
            cmd.ExecuteNonQuery();
        }
    }

}