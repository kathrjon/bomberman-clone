using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;


public class playercontroller : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 startPosition;
    Vector3 destinationCellCenter;
    float walkSpeed = 3.0f;
    [SerializeField] public Rigidbody2D bomb;
    [SerializeField] public Tilemap bg;
    [SerializeField] public LayerMask barrierLayer;


    void Awake()
    {
        startPosition = BMTiles.GetCellCenter(transform.position, this.bg);
        
        this.transform.position = startPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Vector3 cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movePlayer(horizontalInput, verticalInput, cellCenter);

        if (Input.GetKeyDown(KeyCode.E)){
            placeBomb(cellCenter);
        }
    }

    void movePlayer(float horizontalInput, float verticalInput, Vector3 cellCenter){
        if(cellCenter.x == rb.position.x && cellCenter.y == rb.position.y){
            if (horizontalInput != 0 && rb.position.y == cellCenter.y)
            {
                movePlayerHorizontal(horizontalInput, cellCenter);
            }
            else if (verticalInput != 0 && rb.position.x == cellCenter.x)
            {
                movePlayerVertical(verticalInput, cellCenter);
            }
        } else{
            movePlayerTowardsCellCenter(destinationCellCenter);
         }
    }

    void movePlayerHorizontal(float horizontalInput, Vector3 cellCenter){
        if (horizontalInput > 0)
        {
            Vector2 destinationCellCoordinates = new Vector2(cellCenter.x + 1, cellCenter.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, .5f, barrierLayer);
            checkCollidersBeforeMoving(destinationCellCoordinates, hit);
        }
        if (horizontalInput < 0)
        {
            Vector2 destinationCellCoordinates = new Vector2(cellCenter.x - 1, cellCenter.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, .5f, barrierLayer);
            checkCollidersBeforeMoving(destinationCellCoordinates, hit);
        }
    }

    void movePlayerVertical(float verticalInput, Vector3 cellCenter){
        if (verticalInput > 0)
        {
            Vector2 destinationCellCoordinates = new Vector2(cellCenter.x, cellCenter.y + 1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, .5f, barrierLayer);
            checkCollidersBeforeMoving(destinationCellCoordinates, hit);
        }

        if (verticalInput < 0)
        {
            Vector2 destinationCellCoordinates = new Vector2(cellCenter.x, cellCenter.y - 1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .5f, barrierLayer);
            checkCollidersBeforeMoving(destinationCellCoordinates, hit);
        }
    }

    void checkCollidersBeforeMoving(Vector2 destinationCellCoordinates, RaycastHit2D hit){
        if (hit.collider == null)
        {
            moveTowardsNextCell(destinationCellCoordinates);
        }
    }

    void movePlayerTowardsCellCenter(Vector3 cellCenter){
        float distance = walkSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, cellCenter, distance);
    }

    void moveTowardsNextCell(Vector2 adjacentCell){
        destinationCellCenter = BMTiles.GetCellCenter(adjacentCell, bg);
        float distance = walkSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destinationCellCenter, distance);
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
