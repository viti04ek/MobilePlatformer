using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeActivator : MonoBehaviour
{
    public BomberBeeAI BomberBeeAI;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BomberBeeAI.ActivateBee(collision.gameObject.transform.position);
        }
    }
}
