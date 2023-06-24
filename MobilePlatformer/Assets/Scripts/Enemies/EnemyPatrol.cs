using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform LeftBound;
    public Transform RightBound;
    public float Speed;

    public Rigidbody2D Rigidbody2D;
    public SpriteRenderer SpriteRenderer;

    public float MaxDelay;
    public float MinDelay;
    private bool _canTurn;
    private float _originalSpeed;

    public Animator Animator;


    private void Start()
    {
        SetStartDirection();
        _canTurn = true;
    }


    private void Update()
    {
        Move();
        FlipOnEdges();
    }


    private void Move()
    {
        Vector2 temp = Rigidbody2D.velocity;
        temp.x = Speed;
        Rigidbody2D.velocity = temp;
    }


    private void SetStartDirection()
    {
        if (Speed > 0)
            SpriteRenderer.flipX = true;
        else if (Speed < 0)
            SpriteRenderer.flipX = false;
    }


    private void FlipOnEdges()
    {
        if (SpriteRenderer.flipX && transform.position.x >= RightBound.position.x)
        {
            if (_canTurn)
            {
                _canTurn = false;
                _originalSpeed = Speed;
                Speed = 0;
                StartCoroutine("TurnLeft", _originalSpeed);
            }
        }
        else if (!SpriteRenderer.flipX && transform.position.x <= LeftBound.position.x)
        {
            if (_canTurn)
            {
                _canTurn = false;
                _originalSpeed = Speed;
                Speed = 0;
                StartCoroutine("TurnRight", _originalSpeed);
            }
        }
    }


    IEnumerator TurnLeft(float originalSpeed)
    {
        Animator.SetBool("IsIdle", true);
        yield return new WaitForSeconds(Random.Range(MinDelay, MaxDelay));
        Animator.SetBool("IsIdle", false);
        SpriteRenderer.flipX = false;
        Speed = -originalSpeed;
        _canTurn = true;
    }


    IEnumerator TurnRight(float originalSpeed)
    {
        Animator.SetBool("IsIdle", true);
        yield return new WaitForSeconds(Random.Range(MinDelay, MaxDelay));
        Animator.SetBool("IsIdle", false);
        SpriteRenderer.flipX = true;
        Speed = -originalSpeed;
        _canTurn = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(LeftBound.position, RightBound.position);
    }
}
