using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameController.Instance.StopCameraFollow();
            GameController.Instance.ActivateEnemySpawner();
        }
    }
}
