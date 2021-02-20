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
        clone = Instantiate(bomb, rb.position, transform.rotation);
    }
}
