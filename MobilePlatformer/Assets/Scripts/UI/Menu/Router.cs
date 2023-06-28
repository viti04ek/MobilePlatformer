using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
    public void ShowPausePanel()
    {
        GameController.Instance.ShowPausePanel();
    }


    public void HidePausePanel()
    {
        GameController.Instance.HidePausePanel();
    }
}
