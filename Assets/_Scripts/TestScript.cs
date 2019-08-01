using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

public class TestScript : MonoBehaviour {
    // Start is called before the first frame update
    public Text testText;
    void Start () {
        string source = "Hello World!";
        using (SHA256 sha256Hash = SHA256.Create())
        {
            string hash = Hasher.GetHash(sha256Hash, source);

            testText.text += "The SHA256 hash of {source} is: " + hash + "\n";

             testText.text += "\n Verifying the hash...";

            if (Hasher.VerifyHash(sha256Hash, source, hash))
            {
                testText.text += "\n The hashes are the same.";
            }
            else
            {
                testText.text += "\n The hashes are not the same.";
            }
        }
    }
    


    // Update is called once per frame
    void Update () {

    }
}