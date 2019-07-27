using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour {
    // Start is called before the first frame update
    public Image hexagonPause;
    void OnMouseOver () {
        Debug.Log("hello");
        hexagonPause.gameObject.SetActive(true);
    }

    void OnMouseExit(){
        Debug.Log("hello2");
    }
}