using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ActualGameManager : MonoBehaviour {
    // Start is called before the first frame update

    public Tilemap tileMap;
    public TileBase[] Tiles;
    public Color OutlineColor;
    public static int playerTurn = 0;

    void Start () {

    }

    void Awake () {

    }

    void Update () {
        var map = new int[20, 18];

        if (Input.GetMouseButtonDown (0)) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Vector3Int coordinate = tileMap.WorldToCell (mouseWorldPos);
            Vector3 worldloc = tileMap.CellToWorld (coordinate);
            Debug.Log (worldloc);

        }

    }
}