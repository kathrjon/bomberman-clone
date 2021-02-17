using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    float walkSpeed = 2.0f;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 10.0F;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal") * walkSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * walkSpeed;    
    }

    void FixedUpdate(){
        //Vector3 targetVelocity = new Vector2(horizontalMove * 10f, verticalMove * 10f);
        //       rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
        rb.MovePosition(rb.position + movement * walkSpeed * Time.fixedDeltaTime);
    }
}
