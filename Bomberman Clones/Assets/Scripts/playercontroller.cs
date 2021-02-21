using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playercontroller : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    Vector3 startPosition;
    float walkSpeed = 3.0f;
    [SerializeField] public Rigidbody2D bomb;
    [SerializeField] public Tilemap bg;


    void Awake()
    {
        startPosition = getCellCenter(transform.position, bg);
        this.transform.position = startPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Vector3 cellCenter = getCellCenter(transform.position, bg);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movePlayer(horizontalInput, verticalInput, cellCenter);


        if (Input.GetKeyDown(KeyCode.E)){
            placeBomb(cellCenter);
        }
    }

    void FixedUpdate(){
 //       rb.MovePosition(rb.position + movement * walkSpeed * Time.fixedDeltaTime);
    }

    void movePlayer(float horizontalInput, float verticalInput, Vector3 cellCenter){
        if (horizontalInput != 0 && rb.position.y == cellCenter.y) {
            movePlayerHorizontal(horizontalInput, cellCenter);
        } else if (verticalInput != 0 && rb.position.x == cellCenter.x) {
            movePlayerVertical(verticalInput, cellCenter);
        } else{
            movePlayerTowardsCellCenter(cellCenter);
        }
    }

    void movePlayerHorizontal(float horizontalInput, Vector3 cellCenter){
        if (horizontalInput > 0)
        {
            moveTowardsNextCell(cellCenter.x + 1, cellCenter.y);
        }
        if (horizontalInput < 0)
        {
            moveTowardsNextCell(cellCenter.x - 1, cellCenter.y);
        }
    }

    void movePlayerVertical(float verticalInput, Vector3 cellCenter){
        if (verticalInput > 0)
        {
            moveTowardsNextCell(cellCenter.x, cellCenter.y + 1);
        }

        if (verticalInput < 0)
        {
            moveTowardsNextCell(cellCenter.x, cellCenter.y - 1);
        }
    }

    void movePlayerTowardsCellCenter(Vector3 cellCenter){
        float distance = walkSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, cellCenter, distance);
    }

    void moveTowardsNextCell(float adjacentCellX, float adjecentCellY){
        Vector2 adjacentCell = new Vector2(adjacentCellX, adjecentCellY);
        Vector3 adjacentCellCenter = getCellCenter(adjacentCell, bg);
        float distance = walkSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, adjacentCellCenter, distance);
    }

    void placeBomb(Vector3 cellCenter){
        Rigidbody2D clone;
        //       Vector3Int tilePosition = new Vector3Int((int)rb.position.x, (int)rb.position.y, 0);
        //       Tile currentTile = (Tile)bg.GetTile(tilePosition);
        //       Vector2 tileCenter = bg.cellBounds.center;
        //       transform.position = bg.GetCellCenterWorld(cellPosition);
        Debug.Log("-placeBomb- cellCenter" + cellCenter);
        Debug.Log("-placeBomb- transform.rotation" + transform.rotation);
        cellCenter.z = -1;
        clone = Instantiate(bomb, cellCenter, transform.rotation);
    }

    static public Vector3 getCellCenter(Vector2 position, Tilemap bg)
    {
        Vector3Int cellPosition = bg.WorldToCell(position);
        return bg.GetCellCenterWorld(cellPosition);
    }
}
