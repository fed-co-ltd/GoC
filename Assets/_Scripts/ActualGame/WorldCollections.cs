using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldCollections : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap[] Layers;
    public TileBase[] GroundTile;
    public TileBase[] WoodResource;
    public TileBase[] StoneResource;
    public TileBase[] BaseStructures;
    public TileBase[] HouseStructures;
    public TileBase[] FloorTiles;
    public TileBase[] Decorations;
    public TileBase OutlineTile;
    public Color[] OutlineColors;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
