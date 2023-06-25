using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BomberBeeAI : MonoBehaviour
{
    public float DestroyBeeDelay;
    public float Speed;


    public void ActivateBee(Vector3 playerPos)
    {
        transform.DOMove(playerPos, Speed, false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            SFXController.Instance.EnemyExplosion(collision.gameObject.transform.position);
            Destroy(gameObject, DestroyBeeDelay);
        }
    }
}
