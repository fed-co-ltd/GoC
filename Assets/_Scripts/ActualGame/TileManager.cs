using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileManager : TileBase {
    // Start is called before the first frame update
    public Sprite spriteA;
    public Sprite spriteB;

    public override void RefreshTile (Vector3Int position, ITilemap tilemap) {
        for (int yd = -1; yd <= 1; yd++) {
            Vector3Int location = new Vector3Int (position.x, position.y + yd, position.z);
            if (IsNeighbour (location, tilemap))
                tilemap.RefreshTile (location);
        }
        for (int xd = -1; xd <= 1; xd++) {
            Vector3Int location = new Vector3Int (position.x + xd, position.y, position.z);
            if (IsNeighbour (location, tilemap))
                tilemap.RefreshTile (location);
        }
    }

    public override void GetTileData (Vector3Int position, ITilemap tilemap, ref TileData tileData) {
        tileData.sprite = spriteA;
        for (int yd = -1; yd <= 1; yd += 2) {
            Vector3Int location = new Vector3Int (position.x, position.y + yd, position.z);
            if (IsNeighbour (location, tilemap))
                tileData.sprite = spriteB;
        }
        for (int xd = -1; xd <= 1; xd += 2) {
            Vector3Int location = new Vector3Int (position.x + xd, position.y, position.z);
            if (IsNeighbour (location, tilemap))
                tileData.sprite = spriteB;
        }
    }

    private bool IsNeighbour (Vector3Int position, ITilemap tilemap) {
        TileBase tile = tilemap.GetTile (position);
        return (tile != null && tile == this);
    }
}