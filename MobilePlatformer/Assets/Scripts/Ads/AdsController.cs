using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsController : MonoBehaviour
{
    public static AdsController Instance;

    public string AndroidAdMobBannerID; // ca-app-pub-3940256099942544/6300978111
    public bool TestMode;
    private BannerView _bannerView;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {
        RequestBanner();
    }


    public void RequestBanner()
    {
        if (TestMode)
            _bannerView = new BannerView(AndroidAdMobBannerID, AdSize.Banner, AdPosition.Top);

        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerView.LoadAd(adRequest);
        HideBanner();
    }


    public void ShowBanner()
    {
        _bannerView.Show();
    }


    public void HideBanner()
    {
        _bannerView.Hide();
    }


    public void HideBanner(float duration)
    {
        StartCoroutine("HideBannerRoutine", duration);
    }


    private IEnumerator HideBannerRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        _bannerView.Hide();
    }


    private void OnDisable()
    {
        _bannerView.Destroy();
    }
}
