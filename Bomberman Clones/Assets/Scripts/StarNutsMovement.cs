using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class StarNutsMovement : MonoBehaviour
{
    public Vector3 cellCenter;
    [SerializeField] EnemyMovement enemyMovement;
    public Vector3 startPosition;
    [SerializeField] private Tilemap bg;
    [SerializeField] float timeSinceLastReverse;
    [SerializeField] float timeInterval = 10f;
    [SerializeField] private Vector2 newDirection;
    Vector3 lastPos;

    void Start()
    {
        bg = GameObject.Find("TileMap_Background").gameObject.GetComponent<Tilemap>();

//        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
//        startPosition = BMTiles.GetCellCenter(transform.position, bg);
//        this.transform.position = startPosition;

        newDirection = enemyMovement.changeDirection(cellCenter, bg);
    }

    void Update()
    {
        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        var displacement = transform.position - lastPos;
        lastPos = transform.position;
        if (displacement.magnitude == 0)
        {
            enemyMovement.moveEnemy(cellCenter, newDirection, bg);
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
                    newDirection = enemyMovement.reverseDirection(newDirection);
                }
                timeSinceLastReverse = timeInterval;
            }
            enemyMovement.moveEnemy(cellCenter, newDirection, bg);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            newDirection = enemyMovement.changeDirectionAndAvoidEnemyCollision(collision, cellCenter, bg);

        }
        else
        {
            newDirection = enemyMovement.changeDirection(cellCenter, bg);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("explosion"))
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

