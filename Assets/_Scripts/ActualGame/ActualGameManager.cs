using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ActualGameManager : MonoBehaviour {
    // Start is called before the first frame update
    static List<Player> players;
    static WorldCollections Collections;
    public int numPlayers;
    public static int playerTurn = 0;
    public static Dictionary<Vector3Int, bool> OutlineMap;
    public static bool[,,] FogMaps;

    public static Player GetPlayer(){
        return players[playerTurn];
    }
    void Awake () {
        Collections = GameObject.Find ("Collections").GetComponent<WorldCollections> ();
        OutlineMap = new Dictionary<Vector3Int, bool> ();
        FogMaps = new bool[4,18,18]; // where flase means with fog
        World.Instantiate(Collections);
        players = new List<Player>();
    }

    void Start () {
        World.Create();
        var num = numPlayers;
        while (num > 0)
        {
            var d = new PlayerData();
            d.Store("Edrian", "hell-o", 1107657);
            d.Kingdom = 3;
            var p = new Player(d);
            players.Add(p);
            num--;
        }
        World.PlaceBase(players);
        World.ScatterDecor(8);
        World.RefreshMap();
        ClearFog(new Vector3Int(6,6,0));
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Vector3 m = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Vector3Int tileMapPos = Collections.Layers[6].WorldToCell (m);
            if (!World.TroopsMap.ContainsKey (tileMapPos)) {
                PathOutline.ClearMapOutlines (Collections.Layers[6], new int[] {-19, 33, -19, 33 });
                GameManager.isOutlineShown = false;
                OutlineMap.Clear();
            }
        }

    }
    void ClearFog(Vector3Int pos){
        TileBase BlankTile = new Tile();
        PathOutline outline = new PathOutline(Collections.Layers[7], BlankTile);
        outline.ShowPossiblePath(pos);
        Collections.Layers[7].SetTile(pos,BlankTile);
        var hex_pos = outline.GetHexPos();
        foreach (var item in hex_pos)
        {
            FogMaps[0,item.x,item.y] = true;
        }
    }
}