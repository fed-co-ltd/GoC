using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    void Awake () {
        if (instance != null)
        {
            Destroy(gameObject);
        }else{
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

}