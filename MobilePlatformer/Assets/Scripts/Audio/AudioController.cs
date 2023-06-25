using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public PlayerAudio PlayerAudio;
    public AudioFX AudioFX;
    public Transform Player;
    public bool SoundOn;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
