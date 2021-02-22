using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BombermanTools 
{
    class BMTiles {
        public static Vector3 GetCellCenter(Vector2 position, Tilemap bg)
        {
            Vector3Int cellPosition = bg.WorldToCell(position);
            return bg.GetCellCenterWorld(cellPosition);
        }
    }    
}
