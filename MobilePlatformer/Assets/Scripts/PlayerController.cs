using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int SpeedBoost;
    public float JumpSpeed;
    public Rigidbody2D Rigidbody2D;

    public SpriteRenderer SpriteRenderer;


    private void Start()
    {
        
    }

    
    private void Update()
    {
        float playerSpeed = Input.GetAxisRaw("Horizontal");
        if (playerSpeed != 0)
            MoveHorizontal(playerSpeed * SpeedBoost);
        else
            StopMoving();

        if (Input.GetButtonDown("Jump"))
            Jump();
    }


    private void MoveHorizontal(float playerSpeed)
    {
        Rigidbody2D.velocity = new Vector2(playerSpeed, Rigidbody2D.velocity.y);
        
        if (playerSpeed < 0)
            SpriteRenderer.flipX = true;
        else if (playerSpeed > 0)
            SpriteRenderer.flipX = false;
    }


    private void StopMoving()
    {
        Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
    }


    private void Jump()
    {
        Rigidbody2D.AddForce(new Vector2(0, JumpSpeed));
    }
}
