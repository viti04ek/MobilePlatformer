using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monetizer : MonoBehaviour
{
    public bool TimedBanner;
    public float BannerDuration;


    private void Start()
    {
        AdsController.Instance.ShowBanner();
    }


    private void OnDisable()
    {
        if (!TimedBanner)
            AdsController.Instance.HideBanner();
        else
            AdsController.Instance.HideBanner(BannerDuration);
    }
}
