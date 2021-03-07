using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BombermanTools 
{
    
    class BMTiles {
        public static Vector3 GetCellCenter(Vector2 position, Tilemap tm)
        {
            Debug.Log("GetCellCenter position" + position);
            Debug.Log("GetCellCenter tm" + tm);
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
        private float walkSpeed;
        private Transform entity;
        private LayerMask barrierLayer;
        private Tilemap bg;
        private string tag;

        private Transform Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        public LayerMask BarrierLayer
        {
            get { return barrierLayer; }
            set { barrierLayer = value; }
        }

        public Tilemap Bg
        {
            get { return bg; }
            set { bg = value; }
        }

        public string Gameobjecttag
        {
            get { return tag; }
            set { tag = value; }
        }

        public BMMovement(Transform entity, LayerMask barrierLayer, Tilemap bg, string tag)
        {
            this.entity = entity;
            this.barrierLayer = barrierLayer;
            this.bg = bg;
            this.tag = tag;
        }

        public void moveHorizontal(float horizontalInput, Vector3 cellCenter, float speed)
        {
            walkSpeed = speed;

            if (horizontalInput > 0)
            {
                Vector2 destinationCellCoordinates = new Vector2(cellCenter.x + 1, cellCenter.y);
                RaycastHit2D hit = Physics2D.Raycast(entity.position, Vector2.right, .5f, barrierLayer);
                if (tag == "Player")
                {
                    checkCollidersBeforeMoving(destinationCellCoordinates, hit);
                } else
                {
                    Debug.Log("DestinationCellCoordinates: " + destinationCellCoordinates);
                    moveTowardsNextCell(destinationCellCoordinates);
                }
            }

            if (horizontalInput < 0)
            {
                Vector2 destinationCellCoordinates = new Vector2(cellCenter.x - 1, cellCenter.y);
                RaycastHit2D hit = Physics2D.Raycast(entity.position, Vector2.left, .5f, barrierLayer);
                if (tag == "Player")
                {
                    checkCollidersBeforeMoving(destinationCellCoordinates, hit);
                } else
                {
                    Debug.Log("DestinationCellCoordinates: " + destinationCellCoordinates);
                    moveTowardsNextCell(destinationCellCoordinates);
                }
            }
        }

        public void moveVertical(float verticalInput, Vector3 cellCenter, float speed)
        {
            walkSpeed = speed;

            if (verticalInput > 0)
            {
                Vector2 destinationCellCoordinates = new Vector2(cellCenter.x, cellCenter.y + 1);
                RaycastHit2D hit = Physics2D.Raycast(entity.position, Vector2.up, .5f, barrierLayer);
                if (tag == "Player")
                {
                    checkCollidersBeforeMoving(destinationCellCoordinates, hit);
                }
                else
                {
                    Debug.Log("DestinationCellCoordinates: " + destinationCellCoordinates);
                    moveTowardsNextCell(destinationCellCoordinates);
                }
            }

            if (verticalInput < 0)
            {
                Vector2 destinationCellCoordinates = new Vector2(cellCenter.x, cellCenter.y - 1);
                RaycastHit2D hit = Physics2D.Raycast(entity.position, Vector2.down, .5f, barrierLayer);
                if (tag == "Player")
                {
                    checkCollidersBeforeMoving(destinationCellCoordinates, hit);
                }
                else
                {
                    Debug.Log("DestinationCellCoordinates: " + destinationCellCoordinates);
                    moveTowardsNextCell(destinationCellCoordinates);
                }
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
