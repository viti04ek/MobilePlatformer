using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float RestartDelay;

    public GameData GameData;
    private string _dataFilePath;
    private BinaryFormatter BinaryFormatter;

    public Text CoinCount;
    public Text Score;
    public int CoinValue;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        BinaryFormatter = new BinaryFormatter();
        _dataFilePath = Application.persistentDataPath + "/game.dat";
    }


    public void SaveData()
    {
        FileStream fs = new FileStream(_dataFilePath, FileMode.Create);
        BinaryFormatter.Serialize(fs, GameData);
        fs.Close();
    }


    public void LoadData()
    {
        if (File.Exists(_dataFilePath))
        {
            FileStream fs = new FileStream(_dataFilePath, FileMode.Open);
            GameData = (GameData)BinaryFormatter.Deserialize(fs);

            CoinCount.text = $"x {GameData.CoinCount}";
            Score.text = $"Score: {GameData.Score}";

            Debug.Log("Data saved");
            fs.Close();
        }
    }


    private void ResetData()
    {
        FileStream fs = new FileStream(_dataFilePath, FileMode.Create);

        GameData.CoinCount = 0;
        GameData.Score = 0;

        CoinCount.text = $"x {GameData.CoinCount}";
        Score.text = $"Score: {GameData.Score}";
        BinaryFormatter.Serialize(fs, GameData);

        fs.Close();
        Debug.Log("Data reset");
    }


    private void OnEnable()
    {
        Debug.Log("Data loaded");
        LoadData();
    }


    private void OnDisable()
    {
        Debug.Log("Data saved");
        SaveData();
    }


    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);
        Invoke("RestartLevel", RestartDelay);
    }


    public void PlayerDrowned()
    {
        Invoke("RestartLevel", RestartDelay);
    }


    private void RestartLevel()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void UpdateCointCount()
    {
        GameData.CoinCount++;
        CoinCount.text = $"x {GameData.CoinCount}";
        UpdateScore(CoinValue);
    }


    public void UpdateScore(int value)
    {
        GameData.Score += value;
        Score.text = $"Score: {GameData.Score}";
    }
}
