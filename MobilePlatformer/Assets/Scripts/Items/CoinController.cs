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

    public float Speed;
    public bool StartFlying = false;
    private GameObject _coinMeter;


    private void Start()
    {
        if (CoinFX == CoinFX.Fly)
        {
            _coinMeter = GameObject.Find("CoinCount");
        }
    }


    private void Update()
    {
        if (StartFlying)
        {
            transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(_coinMeter.transform.position), Speed);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CoinFX == CoinFX.Vanish)
            {
                Destroy(gameObject);
            }
            else if (CoinFX == CoinFX.Fly)
            {
                gameObject.layer = 0;
                StartFlying = true;
            }
        }
    }
}
