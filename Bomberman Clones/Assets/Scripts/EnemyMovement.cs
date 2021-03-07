using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

    class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private LayerMask barrierLayer;
        [SerializeField] private List<Vector2> possibleDirections = new List<Vector2>();
        [SerializeField] Movement movement;

        public List<Vector2> directionVectors = new List<Vector2> { Vector2.down, Vector2.up, Vector2.right, Vector2.left };


        public List<Vector2> findPossibleDirection()
        {
           Debug.Log("findPossibleDirection");
           for (var i = 0; i < directionVectors.Count; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVectors[i], 1f, barrierLayer);
                    checkDirection(hit, directionVectors[i]);
                }

            return possibleDirections;
        }

        public void moveEnemy(Vector2 cellCenter, Vector2 newDirection, Tilemap bg){
            Debug.Log("moveEnemy cellCenter" + cellCenter);
            Debug.Log("moveEnemy newDirection" + newDirection);
            if (newDirection.x != 0)
                {
                    movement.moveHorizontal(newDirection.x, cellCenter, 1f, bg);
                }
                if (newDirection.y != 0)
                {
                    movement.moveVertical(newDirection.y, cellCenter, 1f, bg);
                }
        }

         public Vector2 pickDirection(List<Vector2> possibleDirectionsList)
        {
            Vector2 newDirection;
            int index = Random.Range(0, possibleDirectionsList.Count);
            newDirection = possibleDirectionsList[index];
            possibleDirectionsList.Clear();

            return newDirection;
        }

        public void checkDirection(RaycastHit2D hit, Vector2 direction)
        {
            Debug.Log("checkDirection");
            if (hit.collider == null)
            {
                possibleDirections.Add(direction);
            }
        }

        public Vector2 reverseDirection(Vector2 newDirection)
        {
            Debug.Log("reverseDirection");
            newDirection = newDirection * - 1;

            return newDirection;
        }
    }

