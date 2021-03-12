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
    Vector2 enemyDirection;
    Vector2 newDirection;

    public List<Vector2> directionVectors = new List<Vector2> { Vector2.down, Vector2.up, Vector2.right, Vector2.left };

    public List<Vector2> findPossibleDirection()
    {
        for (var i = 0; i < directionVectors.Count; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVectors[i], 1f, barrierLayer);
                checkDirection(hit, directionVectors[i]);
            }

        return possibleDirections;
    }

    public void moveEnemy(Vector2 cellCenter, Vector2 newDirection, Tilemap bg){
        if (newDirection.x != 0)
            {
                movement.moveHorizontal(newDirection.x, cellCenter, 1f, bg, barrierLayer);
            }
            if (newDirection.y != 0)
            {
                movement.moveVertical(newDirection.y, cellCenter, 1f, bg, barrierLayer);
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
        if (hit.collider == null)
        {
            possibleDirections.Add(direction);
        }
    }

    public Vector2 reverseDirection(Vector2 newDirection){
        newDirection = newDirection * - 1;

        return newDirection;
    }

    public Vector2 changeDirection(Vector2 cellCenter, Tilemap bg)
    {
        possibleDirections = findPossibleDirection();
        possibleDirections.Remove(enemyDirection);
        enemyDirection = new Vector2();
        if (possibleDirections.Count > 0)
        {
            newDirection = pickDirection(possibleDirections);
            moveEnemy(cellCenter, newDirection, bg);
        }

        possibleDirections.Clear();

        return newDirection;
    }

    public Vector2 changeDirectionAndAvoidEnemyCollision(Collision2D collision, Vector2 cellCenter, Tilemap bg){
        ContactPoint2D contactPoint = collision.GetContact(0);
        enemyDirection.x = contactPoint.point.x - transform.position.x;
        enemyDirection.y = contactPoint.point.y - transform.position.y;
        enemyDirection = enemyDirection.normalized;
        if (Mathf.Abs(enemyDirection.x) > Mathf.Abs(enemyDirection.y))
        {
            float sign = Mathf.Sign(enemyDirection.x);
            enemyDirection.x = 1 * sign;
            enemyDirection.y = 0;
        }
        else
        {
            float sign = Mathf.Sign(enemyDirection.y);
            enemyDirection.y = 1 * sign;
            enemyDirection.x = 0;
        }
        newDirection = changeDirection(cellCenter, bg);

        return newDirection;
    }
}

