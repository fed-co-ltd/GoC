using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class World {
    static WorldCollections Collections;
    public static int size;
    public static Dictionary<Vector3Int, Troop> TroopsMap;
    public static Dictionary<Vector3Int, Floor> FloorMap;
    public static Dictionary<Vector3Int, LayerObject> Map;
    public static Dictionary<Vector3Int, FloorType> DecorMap;
    public static Dictionary<Vector3Int, Resource> ResourceMap;

    public static void Instantiate (WorldCollections collections) {
        Collections = collections;
        TroopsMap = new Dictionary<Vector3Int, Troop> ();
        FloorMap = new Dictionary<Vector3Int, Floor> ();
        Map = new Dictionary<Vector3Int, LayerObject> ();
        ResourceMap = new Dictionary<Vector3Int, Resource> ();
        DecorMap = new Dictionary<Vector3Int, FloorType> ();
    }

    public static void Create (int wsize = 17, bool changeGround = false, int type = 0) {
        size = wsize;
        if (changeGround) {
            Fill (Collections.Layers[0], Collections.GroundTile[type], size);
        }
        Fill (Collections.Layers[1], Collections.FloorTiles[0], size);

    }

    static void Fill (Tilemap map, TileBase tile, int size) {

        for (int i = 0; i <= size; i++) {
            for (int j = 0; j <= size; j++) {
                Vector3Int pos = new Vector3Int (i, j, 0);
                map.SetTile (pos, tile);
            }
        }
    }

    static Vector3Int GetRandomPos (int[] p) {
        while (true) {
            int x = UnityEngine.Random.Range (p[0], p[1]);
            int y = UnityEngine.Random.Range (p[2], p[3]);
            Vector3Int pos = new Vector3Int (x, y, 0);
            if (!Map.ContainsKey (pos)) {
                return pos;
            }
        }
    }
    public static void ScatterDecor (int num) {
        while (num >= 0) {
            var pos = GetRandomPos (new int[] { 0, 17, 0, 17 });
            var floorType = FloorType.Default;
            var floor = new Floor () {
                type = FloorType.Default,
                tile = Collections.FloorTiles[0],
                base_pos = new Vector3Int (0, 0, 0)
            };
            if (!FloorMap.ContainsKey(pos))
            {
                FloorMap.Add (pos, floor);
            }else{
                floorType = FloorMap[pos].type;
            }

            Map.Add (pos, LayerObject.Decorations);
            DecorMap.Add (pos, floorType);
            Collections.Layers[2].SetTile (pos, Collections.Decorations[(int)floorType]);
            num--;
        }

    }

    public static void RefreshMap () {
        Tilemap _map = Collections.Layers[1];
        TileBase _tile = Collections.FloorTiles[0];
        foreach (var item in Map) {

            switch (item.Value) {
                case LayerObject.Base:
                    break;
                case LayerObject.Decorations:
                    _map = Collections.Layers[2];
                    int type = (int) FloorMap[item.Key].type;
                    Debug.Log("Decor Type : " + type);
                    _tile = Collections.Decorations[type];
                    _map.SetTile (item.Key, _tile);
                    break;
                default:
                    break;
            }
        }

    }

    static void GenerateResource (PathOutline outline, Floor floor, int num) {
        int resources = num;
        for (int i = 0; i < resources; i++) {
            var hex_outline = outline.GetHexPos ();
            var n = 3;
            var pos = hex_outline[n];
            var resourceType = Random.Range (1, 10);
            resourceType = resourceType > 5 ? 1 : 2;
            var resourceQuality = Random.Range (1, 10);
            resourceQuality = resourceQuality == 1 ? 4 :
                (resourceQuality > 6 ? 1 : (resourceQuality > 3 ? 2 : 3));
            do {
                n = Random.Range (0, hex_outline.Length);
                pos = hex_outline[n];
            } while (n == 3 || Map.ContainsKey (pos));

            var loot = new Loot () {
                owner = ActualGameManager.GetPlayer ().data.Name,
                isLootable = true,
                Coin = resourceQuality * (int) resourceType,
                ScorePoint = resourceQuality
            };

            var resource = new Resource () {
                floor = floor,
                type = (ResourceType) resourceType,
                Quality = resourceQuality,
                TurnCreated = ActualGameManager.playerTurn,
                loot = loot
            };

            var tile = Collections.WoodResource[0];
            var e = (int) floor.type + ((resourceQuality - 1) * 4);
            Debug.Log ("ResourceType = " + resourceType);
            if ((ResourceType) resourceType == ResourceType.Wood) {
                tile = Collections.WoodResource[e];
            } else {
                tile = Collections.StoneResource[e];
            }
            ResourceMap.Add (pos, resource);
            Map.Add(pos,LayerObject.Resources);
            Collections.Layers[5].SetTile (pos, tile);
        }
    }

    public static void PlaceBase (List<Player> players) {
        List<Vector3Int> base_pos = new List<Vector3Int> ();
        var num = players.Count;
        while (num > 0) {
            var pos = GetRandomPos (new int[] { 1, 16, 1, 16 });
            if (num < players.Count) {
                var iterations = players.Count - num;
                bool IsNearby = true;
                var req = players.Count == 2 ? 10 : (players.Count == 3 ? 8 : 6);

                do {
                    pos = GetRandomPos (new int[] { 1, 16, 1, 16 });
                    IsNearby = false;
                    for (int i = 0; i < iterations; i++) {
                        var test = base_pos[i];
                        var xdist = Mathf.Pow ((pos.x - test.x), 2);
                        var ydist = Mathf.Pow ((pos.y - test.y), 2);
                        var dist = (int) Mathf.Sqrt (xdist + ydist);
                        if (dist < req) {
                            IsNearby = true;
                            Debug.Log ("Nearby");
                        }
                    }
                } while (IsNearby);

            }

            base_pos.Add (pos);
            num--;
        }
        for (int i = 0; i < base_pos.Count; i++) {
            var kingdom = players[i].data.Kingdom;
            Debug.Log (kingdom);
            Map.Add (base_pos[i], LayerObject.Base);
            Collections.Layers[3].SetTile (base_pos[i], Collections.BaseStructures[kingdom]);
            Collections.Layers[1].SetTile (base_pos[i], Collections.FloorTiles[kingdom]);
            var floor = new Floor () {
                type = (FloorType) kingdom,
                tile = Collections.FloorTiles[kingdom],
                base_pos = base_pos[i]
            };

            FullTopOutline outline = new FullTopOutline (Collections.Layers[1], Collections.FloorTiles[kingdom]);
            outline.ShowPossiblePath (base_pos[i]);
            for (int j = 0; j < outline.GetHexPos ().Length; j++) {
                var pos = outline.GetHexPos () [j];
                if (FloorMap.ContainsKey (pos)) {
                    FloorMap[pos] = floor;
                } else {
                    FloorMap.Add (outline.GetHexPos () [j], floor);
                }
            }
            GenerateResource (outline, floor, 4);
        }

    }
}