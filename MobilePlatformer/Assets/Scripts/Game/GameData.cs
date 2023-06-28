using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public int CoinCount;
    public int Score;

    public bool[] KeyFound;

    public int Lives;
    public bool IsFirstBoot;

    public LevelData[] LevelData;

    public bool PlaySound;
    public bool PlayMusic;
}
