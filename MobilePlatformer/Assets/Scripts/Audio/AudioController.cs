using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public PlayerAudio PlayerAudio;
    public AudioFX AudioFX;
    public Transform Player;
    public bool SoundOn = true;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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
}
