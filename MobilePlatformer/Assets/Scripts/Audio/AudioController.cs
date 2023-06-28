using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public PlayerAudio PlayerAudio;
    public AudioFX AudioFX;
    public Transform Player;
    public bool SoundOn;

    public GameObject BGMusic;
    public bool BGMusicOn;

    public GameObject ButtonSound;
    public GameObject ButtonMusic;
    public Sprite SoundOnImage;
    public Sprite SoundOffImage;
    public Sprite MusicOnImage;
    public Sprite MusicOffImage;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    private void Start()
    {
        if (DataController.Instance.GameData.PlayMusic)
        {
            BGMusic.SetActive(true);
            ButtonMusic.GetComponent<Image>().sprite = MusicOnImage;
        }
        else
        {
            BGMusic.SetActive(false);
            ButtonMusic.GetComponent<Image>().sprite = MusicOffImage;
        }

        if (DataController.Instance.GameData.PlaySound)
        {
            SoundOn = true;
            ButtonSound.GetComponent<Image>().sprite = SoundOnImage;
        }
        else
        {
            SoundOn = false;
            ButtonSound.GetComponent<Image>().sprite = SoundOffImage;
        }
    }


    public void PlayerJump(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.PlayerJump, playerPos);
    }


    public void CoinPickup(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.CoinPickup, playerPos);
    }


    public void FireBullets(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.FireBullets, playerPos);
    }


    public void EnemyExplosion(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.EnemyExplosion, playerPos);
    }


    public void BreakCrates(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.BreakCrates, playerPos);
    }


    public void WaterSplash(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.WaterSplash, playerPos);
    }


    public void PowerUp(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.PowerUp, playerPos);
    }


    public void KeyFound(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.KeyFound, playerPos);
    }


    public void EnemyHit(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.EnemyHit, playerPos);
    }


    public void PlayerDied(Vector3 playerPos)
    {
        if (SoundOn)
            AudioSource.PlayClipAtPoint(PlayerAudio.PlayerDied, playerPos);
    }


    public void ToggleSound()
    {
        if (DataController.Instance.GameData.PlaySound)
        {
            SoundOn = false;
            ButtonSound.GetComponent<Image>().sprite = SoundOffImage;
            DataController.Instance.GameData.PlaySound = false;
        }
        else
        {
            SoundOn = true;
            ButtonSound.GetComponent<Image>().sprite = SoundOnImage;
            DataController.Instance.GameData.PlaySound = true;
        }
    }


    public void ToggleMusic()
    {
        if (DataController.Instance.GameData.PlayMusic)
        {
            BGMusic.SetActive(false);
            ButtonMusic.GetComponent<Image>().sprite = MusicOffImage;
            DataController.Instance.GameData.PlayMusic = false;
        }
        else
        {
            BGMusic.SetActive(true);
            ButtonMusic.GetComponent<Image>().sprite = MusicOnImage;
            DataController.Instance.GameData.PlayMusic = true;
        }
    }
}
