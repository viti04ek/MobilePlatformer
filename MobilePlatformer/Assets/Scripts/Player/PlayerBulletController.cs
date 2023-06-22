using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public Vector2 Velocity;


    void Start()
    {
        
    }

    
    void Update()
    {
        Rigidbody2D.velocity = Velocity;
    }
}
