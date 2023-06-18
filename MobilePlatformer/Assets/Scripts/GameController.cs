using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float RestartDelay;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);
        Invoke("RestartLevel", RestartDelay);
    }


    private void RestartLevel()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
