using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public Vector2 Velocity;

    
    private void Update()
    {
        Rigidbody2D.velocity = Velocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameController.Instance.BulletHitEnemy(collision.gameObject.transform);
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
