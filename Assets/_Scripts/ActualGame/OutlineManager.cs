using UnityEngine;
using UnityEngine.Tilemaps;

namespace GoC {
    public class PathOutline {
        protected static TileBase BlankTile;
        protected TileBase Tile;
        protected Tilemap OutlineMap;
        protected Vector3Int[] HexPos = new Vector3Int[7];
        public static void ClearMapOutlines (Tilemap map, int[] pattern) {
            for (int x = pattern[0]; x <= pattern[1]; x++) {
                for (int y = pattern[2]; y <= pattern[3]; y++) {
                    var coord = new Vector3Int (x, y, 0);
                    map.SetTile (coord, BlankTile);
                }
            }
        }

        public void ClearMapOutlines (int[] pattern){
            ClearMapOutlines (this.OutlineMap, pattern);
        }

        public PathOutline (Tilemap map,TileBase tile){
            Tile = tile;
            OutlineMap = map;
        }
        
        protected void FillHexPos () {
            for (int i = 0; i < HexPos.Length; i++)
            {
                if (i == 3) continue;
                OutlineMap.SetTile (HexPos[i], Tile);
            }
        }

        protected void FindHexPos(Vector3Int p){
            var factor = (p.y % 2 == 0) ? -1 : 1;
            var pos = new Vector3Int (p.x - factor, p.y, 0);
            var noneIndex = factor == -1 ? 4 : 2;
            HexPos[3] = p;
            HexPos[noneIndex] = pos;
            for (int i = -1; i <= 1; i++) {
                if (i == 0)
                {
                    HexPos[factor + 3] = new Vector3Int (p.x + factor, p.y, 0);;
                }else if (i == -1)
                {
                    var index = factor == -1 ? 6 : 5;
                    HexPos[index + factor] = new Vector3Int (p.x + factor, p.y - 1, 0); //2
                    HexPos[index] = new Vector3Int (p.x, p.y - 1, 0);
                }else{
                    var index = factor == -1 ? 1 : 0;
                    HexPos[index + factor] = new Vector3Int (p.x + factor, p.y + 1, 0);
                    HexPos[index] = new Vector3Int (p.x, p.y + 1, 0);
                }
            }
        }

        public virtual void ShowPossiblePath(Vector3Int pos){
            FindHexPos(pos);
            FillHexPos();
        }


    }
    public class FlatTopOutline : PathOutline {

        
        public FlatTopOutline (Tilemap map,TileBase tile) : base(map,tile) {
            HexPos = new Vector3Int[13];
        }

        public override void ShowPossiblePath(Vector3Int pos){
            FindHexPos(pos);
            var p = HexPos[2];
            HexPos[7] = new Vector3Int (p.x, p.y+2, 0);
            HexPos[9] = new Vector3Int (p.x-1, p.y, 0);
            HexPos[11] = new Vector3Int (p.x, p.y-2, 0);
            p = HexPos[4];
            HexPos[8] = new Vector3Int (p.x, p.y+2, 0);
            HexPos[10] = new Vector3Int (p.x+1, p.y, 0);
            HexPos[12] = new Vector3Int (p.x, p.y-2, 0);
            FillHexPos();
        }
    }

    public class PointTopOutline : PathOutline
    {
        public PointTopOutline (Tilemap map,TileBase tile) : base(map,tile) {
            HexPos = new Vector3Int[13];
        }
        public override void ShowPossiblePath(Vector3Int pos)
        {
            FindHexPos(pos);
            var p = HexPos[3];
            HexPos[7] = new Vector3Int (p.x, p.y+2, 0);
            HexPos[12] = new Vector3Int (p.x, p.y-2, 0);
            p = HexPos[0];
            HexPos[8] = new Vector3Int (p.x-1, p.y, 0);
            HexPos[9] = new Vector3Int (p.x+2, p.y, 0);
            p = HexPos[5];
            HexPos[10] = new Vector3Int (p.x-1, p.y, 0);
            HexPos[11] = new Vector3Int (p.x+2, p.y, 0);
            FillHexPos();
        }

    }
    public class FullTopOutline : PathOutline
    {
        public FullTopOutline (Tilemap map,TileBase tile) : base(map,tile) {
            HexPos = new Vector3Int[19];
        }
        public override void ShowPossiblePath(Vector3Int pos)
        {
            FindHexPos(pos);
            var p = HexPos[3];
            HexPos[8] = new Vector3Int (p.x, p.y+2, 0);
            HexPos[17] = new Vector3Int (p.x, p.y-2, 0);
            p = HexPos[0];
            HexPos[10] = new Vector3Int (p.x-1, p.y, 0);
            HexPos[11] = new Vector3Int (p.x+2, p.y, 0);
            p = HexPos[5];
            HexPos[14] = new Vector3Int (p.x-1, p.y, 0);
            HexPos[15] = new Vector3Int (p.x+2, p.y, 0);
            p = HexPos[2];
            HexPos[7] = new Vector3Int (p.x, p.y+2, 0);
            HexPos[12] = new Vector3Int (p.x-1, p.y, 0);
            HexPos[16] = new Vector3Int (p.x, p.y-2, 0);
            p = HexPos[4];
            HexPos[9] = new Vector3Int (p.x, p.y+2, 0);
            HexPos[13] = new Vector3Int (p.x+1, p.y, 0);
            HexPos[18] = new Vector3Int (p.x, p.y-2, 0);
            FillHexPos();
        }

    }

}