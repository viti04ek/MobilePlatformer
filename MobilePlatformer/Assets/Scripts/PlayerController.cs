using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int SpeedBoost;
    public float JumpSpeed;
    public Rigidbody2D Rigidbody2D;

    public SpriteRenderer SpriteRenderer;

    public Animator Animator;
    private bool _isJumping;

    private bool _isGrounded;
    public Transform Feet;
    public float BoxWidth;
    public float BoxHeight;
    public LayerMask WhatIsGround;

    private bool _canDoubleJump;
    public float DelayForDoubleJump;

    public Transform LeftBulletSpawn;
    public Transform RightBulletSpawn;
    public GameObject LeftBullet;
    public GameObject RightBullet;

    private bool _leftPressed;
    private bool _rightPressed;

    
    private void Update()
    {
        _isGrounded = Physics2D.OverlapBox(new Vector2(Feet.position.x, Feet.position.y), new Vector2(BoxWidth, BoxHeight), 360f, WhatIsGround);

        float playerSpeed = Input.GetAxisRaw("Horizontal");
        if (playerSpeed != 0)
            MoveHorizontal(playerSpeed * SpeedBoost);
        else
            StopMoving();

        if (Input.GetButtonDown("Jump"))
            Jump();

        ShowFalling();

        if (Input.GetButtonDown("Fire1"))
            FireBullets();

        if (_leftPressed)
            MoveHorizontal(-SpeedBoost);

        if (_rightPressed)
            MoveHorizontal(SpeedBoost);
    }


    private void MoveHorizontal(float playerSpeed)
    {
        Rigidbody2D.velocity = new Vector2(playerSpeed, Rigidbody2D.velocity.y);
        
        if (playerSpeed < 0)
            SpriteRenderer.flipX = true;
        else if (playerSpeed > 0)
            SpriteRenderer.flipX = false;

        if (!_isJumping)
            Animator.SetInteger("State", 1);
    }


    private void StopMoving()
    {
        Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);

        if (!_isJumping)
            Animator.SetInteger("State", 0);
    }


    private void Jump()
    {
        if (_isGrounded)
        {
            _isJumping = true;
            Rigidbody2D.AddForce(new Vector2(0, JumpSpeed));
            Animator.SetInteger("State", 2);

            Invoke("EnableDoubleJump", DelayForDoubleJump);
        }

        if (_canDoubleJump && !_isGrounded)
        {
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.AddForce(new Vector2(0, JumpSpeed));
            Animator.SetInteger("State", 2);
            _canDoubleJump = false;
        }
    }


    private void EnableDoubleJump()
    {
        _canDoubleJump = true;
    }


    private void ShowFalling()
    {
        if (Rigidbody2D.velocity.y<0)
        {
            Animator.SetInteger("State", 3);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _isJumping = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Feet.position, new Vector3(BoxWidth, BoxHeight, 0));
    }


    private void FireBullets()
    {
        if (SpriteRenderer.flipX)
            Instantiate(LeftBullet, LeftBulletSpawn.position, Quaternion.identity);
        else
            Instantiate(RightBullet, RightBulletSpawn.position, Quaternion.identity);
    }


    public void MoveLeft()
    {
        _leftPressed = true;
    }


    public void MoveRight()
    {
        _rightPressed = true;
    }


    public void MoveStop()
    {
        _leftPressed = false;
        _rightPressed = false;
    }
}
