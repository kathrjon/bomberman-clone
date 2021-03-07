using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class StarNutsMovement : MonoBehaviour
{
    public Vector3 cellCenter;
    [SerializeField] EnemyMovement movement;
    private Tilemap bg;
    [SerializeField] float timeSinceLastReverse;
    [SerializeField] float timeInterval = 10f;
    [SerializeField] private Vector2 newDirection;
    [SerializeField] private List<Vector2> possibleDirections = new List<Vector2>();
    Vector3 lastPos;

    void Start()
    {

        bg = GameObject.Find("TileMap_Background").gameObject.GetComponent<Tilemap>();

        lastPos = transform.position;
        timeSinceLastReverse = timeInterval;
        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        Debug.Log("Start cellCenter " + cellCenter);
        Debug.Log("Start transform.position " + transform.position);
        possibleDirections = movement.findPossibleDirection();
        if (possibleDirections.Count>0)
        {
            newDirection = movement.pickDirection(possibleDirections);
        }
        possibleDirections.Clear();
        movement.moveEnemy(cellCenter, newDirection, bg);
    }

    void Update()
    {
        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        Debug.Log("Start cellCenter " + cellCenter);
        Debug.Log("Start transform.position " + transform.position);
        var displacement = transform.position - lastPos;
        lastPos = transform.position;
        if (displacement.magnitude == 0)
        {
            possibleDirections = movement.findPossibleDirection();
            if (possibleDirections.Count > 0)
            {
                newDirection = movement.pickDirection(possibleDirections);
            }
            possibleDirections.Clear();
            movement.moveEnemy(cellCenter, newDirection, bg);
        }
        else
        {
            if (timeSinceLastReverse > 0)
            {
                timeSinceLastReverse -= Time.deltaTime;
            }
            else
            {
                int changeDirectionChance = Random.Range(0, 100);
                if (changeDirectionChance > 75)
                {
                    newDirection = movement.reverseDirection(newDirection);
                }
                timeSinceLastReverse = timeInterval;
            }
            movement.moveEnemy(cellCenter, newDirection, bg);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        possibleDirections = movement.findPossibleDirection();
        if (possibleDirections.Count > 0)
        {
            newDirection = movement.pickDirection(possibleDirections);
        }
        possibleDirections.Clear();
        movement.moveEnemy(cellCenter, newDirection, bg);
    }
}

