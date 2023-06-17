using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController Instance;

    public GameObject SFXCoinPickup;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    
    public void ShowCoinSparkle(Vector3 position)
    {
        Instantiate(SFXCoinPickup, position, Quaternion.identity);
    }
}
