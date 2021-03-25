using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BombermanTools 
{
    
    class BMTiles {
        public static Vector3 GetCellCenter(Vector2 position, Tilemap tm)
        {
            Vector3Int cellPosition = tm.WorldToCell(position);
            return tm.GetCellCenterWorld(cellPosition);
        }

        public static Vector3 GetCellCenter(Vector3Int position, Tilemap tm) 
        {
            return BMTiles.GetCellCenter(new Vector2(position.x, position.y), tm);
        }

        public static void SetTile(Vector3Int position, Tilemap tm, Tile tile) 
        {
            tm.SetTile(position, tile);
        }
    }
}
