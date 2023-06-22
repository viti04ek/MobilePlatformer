using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CoinFX
{
    Vanish,
    Fly
}


public class CoinController : MonoBehaviour
{
    public CoinFX CoinFX;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CoinFX == CoinFX.Vanish)
                Destroy(gameObject);
        }
    }
}
