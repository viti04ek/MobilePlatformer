using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int KeyNumber;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.Instance.UpdateKeyCount(KeyNumber);
            SFXController.Instance.ShowKeySparkle(KeyNumber);
            Destroy(gameObject);
        }
    }
}
