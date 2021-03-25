using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;
using GameStatsTools;

public class BombermanRespawn : MonoBehaviour
{

    public Vector3 startPosition;
    [SerializeField] private Tilemap bg;
    [SerializeField] public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bg = GameObject.Find("TileMap_Background").gameObject.GetComponent<Tilemap>();
        startPosition = BMTiles.GetCellCenter(transform.position, this.bg);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            PlayerDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "explosion")
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Respawn();
    }

    private void Respawn()
    {
        transform.position = startPosition;
    }
}
