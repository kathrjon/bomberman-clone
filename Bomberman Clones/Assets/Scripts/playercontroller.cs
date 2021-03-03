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
    public float walkSpeed = 2.0f;
    public playerStats stats = new playerStats(1,2.0f,1);
    [SerializeField] public Rigidbody2D bomb;
    [SerializeField] public Tilemap bg;
    [SerializeField] public LayerMask barrierLayer;
    private float horizontalInput = 0;
    private float verticalInput = 0;


    void Awake()
    {
        startPosition = BMTiles.GetCellCenter(transform.position, this.bg);
        
        this.transform.position = startPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.E)){
            placeBomb(cellCenter);
        }
    }

    private void FixedUpdate()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            movePlayer(horizontalInput, verticalInput, cellCenter);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void movePlayer(float horizontalInput, float verticalInput, Vector3 cellCenter){

        BMMovement move = new BMMovement();
            float distanceFromCenterY = rb.position.y - cellCenter.y;
            if (horizontalInput != 0 && Mathf.Abs(distanceFromCenterY) < .5f){
                move.moveHorizontal(horizontalInput, cellCenter, walkSpeed, transform, barrierLayer, bg);
            }
            float distanceFromCenterX = rb.position.x - cellCenter.x;
            if (verticalInput != 0 && Mathf.Abs(distanceFromCenterX) < .5f){
                move.moveVertical(verticalInput, cellCenter, walkSpeed, transform, barrierLayer, bg);
            }
    }

    void placeBomb(Vector3 cellCenter){
        Rigidbody2D clone;
        cellCenter.z = -1;
        clone = Instantiate(bomb, cellCenter, transform.rotation);
    }
    void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Vector3 direction = transform.TransformDirection(Vector3.right) * .5f;
       Gizmos.DrawRay(transform.position, direction);
    }
}
