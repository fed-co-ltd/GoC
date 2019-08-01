using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.UI;

public class ActualGameManager : MonoBehaviour {
    // Start is called before the first frame update
  
    
    public Grid grid;
    public static int playerTurn = 0;
    void Start () {
    }

    void Awake(){

    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Vector3Int coordinate = grid.WorldToCell (mouseWorldPos);
            Debug.Log (coordinate);
        }
    }


    
}