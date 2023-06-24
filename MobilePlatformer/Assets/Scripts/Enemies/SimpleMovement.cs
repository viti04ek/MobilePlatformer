using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Rigidbody2D;
    public SpriteRenderer SpriteRenderer;


    private void Start()
    {
        SetStartingDirection();
    }


    private void Update()
    {
        Move();
    }


    private void Move()
    {
        Vector2 temp = Rigidbody2D.velocity;
        temp.x = Speed;
        Rigidbody2D.velocity = temp;
    }


    private void SetStartingDirection()
    {
        if (Speed > 0)
            SpriteRenderer.flipX = true;
        else if (Speed < 0)
            SpriteRenderer.flipX = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            FlipOnCollision();
    }


    private void FlipOnCollision()
    {
        Speed = -Speed;
        SetStartingDirection();
    }
}
