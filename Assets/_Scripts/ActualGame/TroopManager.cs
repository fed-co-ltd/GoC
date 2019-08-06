using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TroopManager : MonoBehaviour {
    // Start is called before the first frame update
    // Update is called once per frame  
    Vector3Int GridPosition;
    Vector3 FootPosition;
    static WorldCollections Collections;

    public bool isOutlineShown = false;
    public Troop ObjectData;
    void Start () {
        Refresh();
        ObjectData = new Troop(TroopClass.Melee);
        World.TroopsMap.Add(GridPosition,ObjectData);
    }

    void Update () {
        isOutlineShown = GameManager.isOutlineShown;
        if (Input.GetMouseButtonDown (0) && !isOutlineShown) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            if (IsInside (mouseWorldPos)) {
                Debug.Log(gameObject.name);
                ShowPossiblePath ();
            }
        }
    }

    public void ShowPossiblePath () {
        var outlineColors = Collections.OutlineColors;
        var outline = (Tile) Collections.OutlineTile;
        outline.color = outlineColors[0];
        var outlineMap = Collections.Layers[6];
        var ed = new FullTopOutline (outlineMap, outline);
        PathOutline.ClearMapOutlines (outlineMap, new int[] {-19, 33, -19, 33 });
        ActualGameManager.OutlineMap.Clear();
        ed.ShowPossiblePath (GridPosition);
        GameManager.isOutlineShown = true;
    }
    void Refresh () {
        Collections = GameObject.Find ("Collections").GetComponent<WorldCollections> ();
        var p = transform.position;
        var s = transform.localScale;
        FootPosition = new Vector3 (p.x, p.y - s.y,p.z);
        GridPosition = Collections.Layers[6].WorldToCell (FootPosition);
    }

    bool IsInside (Vector3 pos) {
        Vector3Int mouseCoord = Collections.Layers[6].WorldToCell (pos);
        Refresh();
        if (mouseCoord == GridPosition)
        {
            return true;
        }
        return false;
    }
}