using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController Instance;

    public SFX SFX;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    
    public void ShowCoinSparkle(Vector3 position)
    {
        Instantiate(SFX.SFXCoinPickup, position, Quaternion.identity);
    }
}
