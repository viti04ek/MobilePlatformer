using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


[Serializable]
public class UI
{
    [Header("Text")]
    public Text CoinCount;
    public Text Score;
    public Text Timer;

    [Header("Images/Sprites")]
    public Image Key0;
    public Image Key1;
    public Image Key2;
    public Sprite Key0Full;
    public Sprite Key1Full;
    public Sprite Key2Full;
    public Image Heart1;
    public Image Heart2;
    public Image Heart3;
    public Sprite EmptyHeart;
    public Sprite FullHeart;

    public GameObject GameOver;

    public Slider BossHealth;
}
