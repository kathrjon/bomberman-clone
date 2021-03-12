using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class Movement : MonoBehaviour
{
    private float walkSpeed;

    public void moveHorizontal(float horizontalInput, Vector3 cellCenter, float speed, Tilemap bg, LayerMask barrierLayer){
        walkSpeed = speed;

        if (horizontalInput > 0){
           Vector2 destinationCellCoordinates = new Vector2(cellCenter.x + 1, cellCenter.y);
           RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, .5f, barrierLayer);
           if (tag == "Player"){
              checkCollidersBeforeMoving(destinationCellCoordinates, hit, bg);
           }
           else{
              moveTowardsNextCell(destinationCellCoordinates, bg);
           }
        }

        if (horizontalInput < 0){
            Vector2 destinationCellCoordinates = new Vector2(cellCenter.x - 1, cellCenter.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, .5f, barrierLayer);
            if (tag == "Player"){
                checkCollidersBeforeMoving(destinationCellCoordinates, hit, bg);
            }
            else{
                moveTowardsNextCell(destinationCellCoordinates, bg);
            }
        }
    }

    public void moveVertical(float verticalInput, Vector3 cellCenter, float speed, Tilemap bg, LayerMask barrierLayer)
    {
        walkSpeed = speed;

        if (verticalInput > 0){
            Vector2 destinationCellCoordinates = new Vector2(cellCenter.x, cellCenter.y + 1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, .5f, barrierLayer);
            if (tag == "Player"){
                checkCollidersBeforeMoving(destinationCellCoordinates, hit, bg);
            }
            else{
                moveTowardsNextCell(destinationCellCoordinates, bg);
            }
        }

        if (verticalInput < 0){
            Vector2 destinationCellCoordinates = new Vector2(cellCenter.x, cellCenter.y - 1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .5f, barrierLayer);
            if (tag == "Player"){
                checkCollidersBeforeMoving(destinationCellCoordinates, hit, bg);
            }
            else{
                moveTowardsNextCell(destinationCellCoordinates, bg);
            }
        }
    }

    void checkCollidersBeforeMoving(Vector2 destinationCellCoordinates, RaycastHit2D hit, Tilemap bg)
    {
        if (hit.collider == null){
            moveTowardsNextCell(destinationCellCoordinates, bg);
        }
    }

    void moveTowardsNextCell(Vector2 adjacentCell, Tilemap bg)
    {
        Vector3 destinationCellCenter = BMTiles.GetCellCenter(adjacentCell, bg);
        float distance = walkSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destinationCellCenter, distance);
    }
}
