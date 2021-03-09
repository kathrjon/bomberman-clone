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

        public static void SetTile(Vector3Int position, Tilemap tm, Tile tile) 
        {
            tm.SetTile(position, tile);
        }
    }
}
