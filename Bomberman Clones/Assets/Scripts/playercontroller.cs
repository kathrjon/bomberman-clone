using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;
using GameStatsTools;


public class playercontroller : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector3 startPosition;
    public Vector3 cellCenter;
    public playerStats stats;
    [SerializeField] public Rigidbody2D bomb;
    [SerializeField] private Tilemap bg;
    [SerializeField] public LayerMask barrierLayer;
    private float horizontalInput = 0;
    private float verticalInput = 0;
    [SerializeField] Movement movement;

    void Awake()
    {
        stats = new playerStats(1, 2.0f, 1);
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        bg = GameObject.Find("TileMap_Background").gameObject.GetComponent<Tilemap>();
        startPosition = BMTiles.GetCellCenter(transform.position, this.bg);
        startPosition.z = 1;
        this.transform.position = startPosition;
    }

    void Update(){
        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.E)){
            if(GameObject.FindGameObjectsWithTag("Bomb").Length < stats.bombCount)
            {
                placeBomb(cellCenter);
            }
        }
    }

    private void FixedUpdate()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            movePlayer(horizontalInput, verticalInput, cellCenter);
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        Debug.Log("Collider: " + col.tag);
        if (col.tag == "FireUp")
        {
            stats.explosionStrength = stats.changeExplosionStrength(stats, 1);
            Debug.Log("Fire Power Now: " + stats.explosionStrength);
            Destroy(col.gameObject);
        }
        if (col.tag == "BombUp")
        {
            stats.bombCount = stats.changeBombCount(stats, 1);
            Debug.Log("Fire Power Now: " + stats.explosionStrength);
            Destroy(col.gameObject);
        }
        if (col.tag == "SpeedUp")
        {
            stats.walkSpeed = stats.changeWalkSpeed(stats, 1);
            Debug.Log("Fire Power Now: " + stats.explosionStrength);
            Destroy(col.gameObject);
        }
    }

    void movePlayer(float horizontalInput, float verticalInput, Vector3 cellCenter){

        float distanceFromCenterY = rb.position.y - cellCenter.y;
        if (horizontalInput != 0 && Mathf.Abs(distanceFromCenterY) < .5f){
            movement.moveHorizontal(horizontalInput, cellCenter, stats.walkSpeed, bg);
        }

        float distanceFromCenterX = rb.position.x - cellCenter.x;
        if (verticalInput != 0 && Mathf.Abs(distanceFromCenterX) < .5f){
            movement.moveVertical(verticalInput, cellCenter, stats.walkSpeed, bg);
        }

    }

    void placeBomb(Vector3 cellCenter){
        Rigidbody2D clone;
        cellCenter.z = -1;
        clone = Instantiate(bomb, cellCenter, transform.rotation);
    }
}
