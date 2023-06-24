using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController Instance;

    public SFX SFX;

    public Transform Key0;
    public Transform Key1;
    public Transform Key2;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    
    public void ShowCoinSparkle(Vector3 position)
    {
        Instantiate(SFX.SFXCoinPickup, position, Quaternion.identity);
    }


    public void ShowKeySparkle(int keyNumber)
    {
        Vector3 position = Vector3.zero;
        if (keyNumber == 0)
            position = Key0.position;
        else if (keyNumber == 1)
            position = Key1.position;
        else if (keyNumber == 2)
            position = Key2.position;

        Instantiate(SFX.SFXBulletPickup, position, Quaternion.identity);
    }


    public void ShowBulletSparkle(Vector3 position)
    {
        Instantiate(SFX.SFXBulletPickup, position, Quaternion.identity);
    }


    public void ShowPlayerLanding(Vector3 position)
    {
        Instantiate(SFX.SFXPlayerLands, position, Quaternion.identity);
    }


    public void HandleBoxBreaking(Vector3 position)
    {
        Vector3 pos1 = position;
        pos1.x -= 0.3f;

        Vector3 pos2 = position;
        pos2.x += 0.3f;

        Vector3 pos3 = position;
        pos3.x -= 0.3f;
        pos3.y -= 0.3f;

        Vector3 pos4 = position;
        pos4.x += 0.3f;
        pos4.y += 0.3f;

        Instantiate(SFX.SFXFragment1, pos1, Quaternion.identity);
        Instantiate(SFX.SFXFragment2, pos2, Quaternion.identity);
        Instantiate(SFX.SFXFragment2, pos3, Quaternion.identity);
        Instantiate(SFX.SFXFragment1, pos4, Quaternion.identity);
    }


    public void ShowSplash(Vector3 position)
    {
        Instantiate(SFX.SFXSplash, position, Quaternion.identity);
    }
}
