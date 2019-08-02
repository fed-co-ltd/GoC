using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ActualGameManager : MonoBehaviour {
    // Start is called before the first frame update

    public Tilemap tileMap;
    public TileBase TileA;
    private TileBase TileB;
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
            FullTopOutline ed = new FullTopOutline(tileMap,TileA);
            PathOutline.ClearMapOutlines(tileMap,new int[]{0,17,0,17});
            ed.ShowPossiblePath(coordinate);
        }

    }

    void ShowOutlinesA (Vector3Int p) {
        var factor = (p.y % 2 == 0) ? -1 : 1;
        var tile = (Tile) TileB;
        var pos = p;
        for (int x = 2; x < 20; x++) {
            for (int y = 0; y < 18; y++) {
                var coord = new Vector3Int (x, y, 0);
                tileMap.SetTile (coord, tile);
            }
        }
        tile = (Tile) TileA;
        tile.color = OutlineColor;
        pos = new Vector3Int (p.x - factor, p.y, 0);
        tileMap.SetTile (pos, tile);
        for (int i = -1; i <= 1; i++) {
            pos = new Vector3Int (p.x, p.y + i, 0);
            if (i == 0)
            {
                tileMap.SetTile (pos, tile);
                tileMap.SetTile (new Vector3Int (pos.x-(factor*2), pos.y, 0), tile);
            }else{
                 GenerateOutlineA (p,pos, tile, factor, i, 1);
            }
            
            pos = new Vector3Int (p.x + factor, p.y + i, 0);
            if (i == 0)
            {
                tileMap.SetTile (pos, tile);
                Debug.Log(p);
                tileMap.SetTile (new Vector3Int (pos.x+factor, pos.y, 0), tile);
            }else{
                 GenerateOutlineA (p,pos, tile, -factor, i, 1);
            }
        }
        tileMap.SetTile (p, TileB);
    }

    void GenerateOutlineA (Vector3Int p,Vector3Int s, TileBase tile, int xdirection, int ydirection, int number) {
        Vector3Int pos = s;
        var diff = (s.x % 2 == 0) ? 1 : 0;
        var xdiff = 0;
        
        if (p.x % 2 != 0 && p.y % 2 !=0)
        {
            xdiff = -1;
        }else if (p.x % 2 != 0)
        {
            xdiff = 1;
        }else{
           xdiff = 0;
        }
    
        tileMap.SetTile (s, tile);
        for (int i = 1; i <= number; i++) {
            int x = (((int) Function.Round (i / 2)) - diff) * xdirection;
            int y = i * ydirection;

            
        
            pos = new Vector3Int (s.x + x + xdiff , s.y + y, 0);
            tileMap.SetTile (pos, tile);
        }

    }

    void ShowOutlineB (Vector3Int p){

    }
}