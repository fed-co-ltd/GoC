using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using GoC;

public class TestScript : MonoBehaviour {
    // Start is called before the first frame update
    public Text testText;
    void Start () {
        testText.text = "";
        testText.text += Function.Round(1.499f);
    }
    


    // Update is called once per frame
    void Update () {

    }
}