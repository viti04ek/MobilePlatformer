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

    public int CoinValue;
    public UI UI;


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

            UI.CoinCount.text = $"x {GameData.CoinCount}";
            UI.Score.text = $"Score: {GameData.Score}";

            Debug.Log("Data saved");
            fs.Close();
        }
    }


    private void ResetData()
    {
        FileStream fs = new FileStream(_dataFilePath, FileMode.Create);

        GameData.CoinCount = 0;
        GameData.Score = 0;
        for (int keyNumber = 0; keyNumber < 3; keyNumber++)
        {
            GameData.KeyFound[keyNumber] = false;
        }

        UI.CoinCount.text = $"x {GameData.CoinCount}";
        UI.Score.text = $"Score: {GameData.Score}";

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
        UI.CoinCount.text = $"x {GameData.CoinCount}";
        UpdateScore(CoinValue);
    }


    public void UpdateScore(int value)
    {
        GameData.Score += value;
        UI.Score.text = $"Score: {GameData.Score}";
    }


    public void UpdateKeyCount(int keyNumber)
    {
        GameData.KeyFound[keyNumber] = true;

        if (keyNumber == 0)
            UI.Key0.sprite = UI.Key0Full;
        else if (keyNumber == 1)
            UI.Key1.sprite = UI.Key1Full;
        else if (keyNumber == 2)
            UI.Key2.sprite = UI.Key2Full;
    }
}
