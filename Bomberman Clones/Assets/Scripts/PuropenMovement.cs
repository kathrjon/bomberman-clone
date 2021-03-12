using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class PuropenMovement : MonoBehaviour
{
    public Vector3 cellCenter;
    [SerializeField] EnemyMovement enemyMovement;
    public Vector3 startPosition;
    [SerializeField] private Tilemap bg;
    [SerializeField] private Vector2 newDirection;

    void Start()
    {
        bg = GameObject.Find("TileMap_Background").gameObject.GetComponent<Tilemap>();

        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        startPosition = BMTiles.GetCellCenter(transform.position, bg);
        startPosition.z = 1;
        this.transform.position = startPosition;

        newDirection = enemyMovement.changeDirection(cellCenter, bg);
    }

    void Update()
    {
        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        enemyMovement.moveEnemy(cellCenter, newDirection, bg);
    }

    private void OnCollisionEnter2D(Collision2D collision){
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

    private void Die() {
        Destroy(gameObject);
    }
}
