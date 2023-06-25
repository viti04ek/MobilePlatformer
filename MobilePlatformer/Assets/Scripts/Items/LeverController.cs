using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public Vector2 JumpSpeed;
    public GameObject[] Stairs;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D.AddForce(JumpSpeed);

            foreach (var stair in Stairs)
            {
                stair.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
