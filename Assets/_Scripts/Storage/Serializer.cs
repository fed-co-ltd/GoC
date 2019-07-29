using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using GoC;

public class Serializer {
    BinaryFormatter binaryFormatter;
    public static string SavePathDirectory;
    string SaveFileName;
    string SavePath;

    object DataObject;

    public string saveFileName { get => SaveFileName; set => SaveFileName = value; }
    public object dataObject { get => DataObject; set => DataObject = value; }

    public Serializer () {
        binaryFormatter = new BinaryFormatter ();
    }

    public Serializer Initialize () {
        if (!Directory.Exists (SavePathDirectory)) {
            Directory.CreateDirectory (SavePathDirectory);
        }
        SavePath = SavePathDirectory + SaveFileName;
        return this;
    }

    public void SaveData (object data) {
        using (var fileStream = File.Create (SavePath)) {
            binaryFormatter.Serialize (fileStream, data);
        }
    }

    public object LoadData (object NewDataObject) {
        if (File.Exists (SavePath)) {
            using (var fileStream = File.Open (SavePath, FileMode.Open)) {
                NewDataObject = binaryFormatter.Deserialize(fileStream);
            }
        } else {
            Debug.LogWarning("Save file doesn't exist.");
        }

        return NewDataObject;
    }

}