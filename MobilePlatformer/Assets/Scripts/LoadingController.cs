using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    public static LoadingController Instance;

    public GameObject PanelLoading;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void ShowLoading()
    {
        PanelLoading.SetActive(true);
    }
}
