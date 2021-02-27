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

    class BMMovement
    {
        float walkSpeed;
        Transform entity;
        LayerMask barrierLayer;
        Tilemap bg;

        public void moveHorizontal(float horizontalInput, Vector3 cellCenter, float speed, Transform transform, LayerMask layer, Tilemap tilemap)
        {
            walkSpeed = speed;
            barrierLayer = layer;
            entity = transform;
            bg = tilemap;
            if (horizontalInput > 0)
            {
                Vector2 destinationCellCoordinates = new Vector2(cellCenter.x + 1, cellCenter.y);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, .5f, barrierLayer);
                checkCollidersBeforeMoving(destinationCellCoordinates, hit);
            }
            if (horizontalInput < 0)
            {
                Vector2 destinationCellCoordinates = new Vector2(cellCenter.x - 1, cellCenter.y);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, .5f, barrierLayer);
                checkCollidersBeforeMoving(destinationCellCoordinates, hit);
            }
        }

        public void moveVertical(float verticalInput, Vector3 cellCenter, float speed, Transform transform, LayerMask layer, Tilemap tilemap)
        {
            walkSpeed = speed;
            barrierLayer = layer;
            entity = transform;
            bg = tilemap;
            if (verticalInput > 0)
            {
                Vector2 destinationCellCoordinates = new Vector2(cellCenter.x, cellCenter.y + 1);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, .5f, barrierLayer);
                checkCollidersBeforeMoving(destinationCellCoordinates, hit);
            }

            if (verticalInput < 0)
            {
                Vector2 destinationCellCoordinates = new Vector2(cellCenter.x, cellCenter.y - 1);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .5f, barrierLayer);
                checkCollidersBeforeMoving(destinationCellCoordinates, hit);
            }
        }

        void checkCollidersBeforeMoving(Vector2 destinationCellCoordinates, RaycastHit2D hit)
        {
            if (hit.collider == null)
            {
                moveTowardsNextCell(destinationCellCoordinates);
            }
        }

        void moveTowardsNextCell(Vector2 adjacentCell)
        {
            Vector3 destinationCellCenter = BMTiles.GetCellCenter(adjacentCell, bg);
            float distance = walkSpeed * Time.deltaTime;
            entity.position = Vector3.MoveTowards(entity.position, destinationCellCenter, distance);
        }
    }
    
}
