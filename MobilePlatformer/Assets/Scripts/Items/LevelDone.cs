using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.Instance.LevelComplete();
        }
    }
}
