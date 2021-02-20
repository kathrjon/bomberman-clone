using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playercontroller : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    float walkSpeed = 2.0f;
    [SerializeField] public Rigidbody2D bomb;
    [SerializeField] public Tilemap bg;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal") * walkSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * walkSpeed;

        if (Input.GetKeyDown(KeyCode.E)){
            placeBomb();
        }
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * walkSpeed * Time.fixedDeltaTime);
    }

    void placeBomb(){
        Rigidbody2D clone;
 //       Vector3Int tilePosition = new Vector3Int((int)rb.position.x, (int)rb.position.y, 0);
 //       Tile currentTile = (Tile)bg.GetTile(tilePosition);
 //       Vector2 tileCenter = bg.cellBounds.center;
        Vector3Int cellPosition = bg.WorldToCell(transform.position);
        Vector3 cellCenter = bg.GetCellCenterWorld(cellPosition);
 //       transform.position = bg.GetCellCenterWorld(cellPosition);
        clone = Instantiate(bomb, cellCenter, transform.rotation);
    }
}
