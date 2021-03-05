using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BombermanTools;

public class PuropenMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector3 cellCenter;
    [SerializeField] public Tilemap bg;
    [SerializeField] List<Vector2> possibleDirections = new List<Vector2>();
    [SerializeField] LayerMask barrierLayer;
    [SerializeField] Vector2 newDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //detect possible movement
        findPossibleDirection();
        pickDirection();
        moveEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        cellCenter = BMTiles.GetCellCenter(transform.position, bg);
        moveEnemy();
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        findPossibleDirection();
        pickDirection();
        moveEnemy();
    }

    private void findPossibleDirection()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1f, barrierLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f, barrierLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, barrierLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, barrierLayer);

        checkDirection(hitDown, Vector2.down);
        checkDirection(hitUp, Vector2.up);
        checkDirection(hitRight, Vector2.right);
        checkDirection(hitLeft, Vector2.left);
    }

    private void moveEnemy()
    {
        BMMovement move = new BMMovement(transform, barrierLayer, bg, gameObject.tag);
        if (newDirection.x != 0)
        {
            Debug.Log("newDirection.x " + newDirection.x);
            Debug.Log("Move on X axis");
            move.moveHorizontal(newDirection.x, cellCenter, 1f);
        }
        if (newDirection.y != 0)
        {
            Debug.Log("newDirection.y " + newDirection.y);
            Debug.Log("Move on Y axis");
            move.moveVertical(newDirection.y, cellCenter, 1f);
        }
    }

    private void pickDirection()
    {
        Debug.Log("Enter Pick Direction");
        Debug.Log("possibleDirections.Count " + possibleDirections.Count);
        if (possibleDirections.Count >= 1)
        {
            for (var i = 0; i< possibleDirections.Count; i++)
            {
                Debug.Log("possibleDirections " + i + " " + possibleDirections[i]);
            }
            int index = Random.Range(0, possibleDirections.Count);
            newDirection = possibleDirections[index];
            possibleDirections.Clear();
            Debug.Log("New Direction" + newDirection);
        }
    }

    private void checkDirection(RaycastHit2D hit, Vector2 direction)
    {
        if(hit.collider == null)
        {
            Debug.Log("Add Vector to List" + direction);
            possibleDirections.Add(direction);
        }
    }
}
