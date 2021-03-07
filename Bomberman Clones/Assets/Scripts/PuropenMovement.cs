using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class PuropenMovement : MonoBehaviour
{
    public Vector3 cellCenter;
    [SerializeField] EnemyMovement movement;
    private Tilemap bg;
    [SerializeField] private Vector2 newDirection;
    [SerializeField] private List<Vector2> possibleDirections = new List<Vector2>();

    void Start()
    {
        bg = GameObject.Find("TileMap_Background").gameObject.GetComponent<Tilemap>();

        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        possibleDirections = movement.findPossibleDirection();
        newDirection = movement.pickDirection(possibleDirections);
        movement.moveEnemy(cellCenter, newDirection, bg);
    }

    void Update()
    {
        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        Debug.Log(cellCenter);
        Debug.Log(newDirection);
        Debug.Log(bg);
        movement.moveEnemy(cellCenter, newDirection, bg);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        possibleDirections = movement.findPossibleDirection();
        newDirection = movement.pickDirection(possibleDirections);
        movement.moveEnemy(cellCenter, newDirection, bg);
    }

}
