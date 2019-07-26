using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.AccessControl;
using Mono.Data.Sqlite;
using UnityEngine;

public class DataSaver {
    private string ConnectionString;
    private string SavePathDirectory;
    private string DBfileName;
    private SqliteConnection DBconnection;
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
        ConnectionString = "Data Source=" + SavePathDirectory + DBfileName;
        if (!System.IO.File.Exists (SavePathDirectory + DBfileName)) {
            CreateDataBase (SavePathDirectory + DBfileName);
        }
        DBconnection = new SqliteConnection (ConnectionString);
        DBconnection.Open ();
        return this;
    }

    public void CreateDataBase (string fileName) {
        FileStream file = new FileStream(fileName,
                                         FileMode.Create,
                                         FileAccess.ReadWrite,
                                         FileShare.ReadWrite,
                                         4096,
                                         true
                                         );
        file.Close();
    }

    public void Close () {
        DBconnection.Close ();
    }

    public IDataReader Select (string command) {
        using (SqliteCommand cmd = DBconnection.CreateCommand ()) {
            DBcommandText = "SELECT " + command;
            cmd.CommandText = DBcommandText;
            var reader = cmd.ExecuteReader ();
            return reader;            
        }

    }

    public bool Insert (string command) {
        using (SqliteCommand cmd = DBconnection.CreateCommand ()) {
            DBcommandText = "INSERT INTO " + command;
            cmd.CommandText = DBcommandText;
            if (cmd.ExecuteNonQuery () > 0) {
                return true;
            }
            return false;
        }
    }

    public bool Update (string command) {
        using (SqliteCommand cmd = DBconnection.CreateCommand ()) {
            DBcommandText = "UPDATE " + command;
            cmd.CommandText = DBcommandText;
            if (cmd.ExecuteNonQuery () > 0) {
                return true;
            }
            return false;
        }
    }

    public void CreateTable (string command) {
        using (SqliteCommand cmd = DBconnection.CreateCommand ()) {
            DBcommandText = "CREATE TABLE IF NOT EXISTS " + command;
            cmd.CommandText = DBcommandText;
            cmd.ExecuteNonQuery();
        }
    }

}