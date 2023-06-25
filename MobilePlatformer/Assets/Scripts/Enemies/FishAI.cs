using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float JumpSpeed;
    public Rigidbody2D Rigidbody2D;
    public SpriteRenderer SpriteRenderer;


    private void Start()
    {
        FishJump();
    }


    private void Update()
    {
        if (Rigidbody2D.velocity.y > 0)
            SpriteRenderer.flipY = false;
        else
            SpriteRenderer.flipY = true;
    }


    public void FishJump()
    {
        Rigidbody2D.AddForce(new Vector2(0, JumpSpeed));
    }
}
