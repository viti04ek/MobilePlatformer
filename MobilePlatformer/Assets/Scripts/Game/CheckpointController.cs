using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("CPX", collision.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("CPY", collision.gameObject.transform.position.y);
        }
    }
}
