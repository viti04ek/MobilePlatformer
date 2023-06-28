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


    public void ToggleSound()
    {
        AudioController.Instance.ToggleSound();
    }


    public void ToggleMusic()
    {
        AudioController.Instance.ToggleMusic();
    }
}
