using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController Instance = null;
    public GameData GameData;
    private string _dataFilePath;
    private BinaryFormatter BinaryFormatter;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        BinaryFormatter = new BinaryFormatter();
        _dataFilePath = Application.persistentDataPath + "/game.dat";
        Debug.Log(_dataFilePath);
    }


    public void RefreshData()
    {
        if (File.Exists(_dataFilePath))
        {
            FileStream fileStream = new FileStream(_dataFilePath, FileMode.Open);
            GameData = (GameData)BinaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            Debug.Log("Data refresh");
        }
    }


    private void OnEnable()
    {
        CheckDB();
    }


    private void CheckDB()
    {
        if (!File.Exists(_dataFilePath))
        {
            #if UNITY_ANDROID
            string srcFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
            WWW downloader = new WWW(srcFile);
            while (!downloader.isDone)
            {

            }
            File.WriteAllBytes(_dataFilePath, downloader.bytes);
            RefreshData();
            #endif
        }
        else
        {
            RefreshData();
        }
    }


    public void SaveData()
    {
        FileStream fileStream = new FileStream(_dataFilePath, FileMode.Create);
        BinaryFormatter.Serialize(fileStream, GameData);
        fileStream.Close();
        Debug.Log("Data saved");
    }


    public void SaveData(GameData data)
    {
        FileStream fileStream = new FileStream(_dataFilePath, FileMode.Create);
        BinaryFormatter.Serialize(fileStream, GameData);
        fileStream.Close();
        Debug.Log("Data saved");
    }


    public bool IsUnlocked(int levelNumber)
    {
        return GameData.LevelData[levelNumber].IsUnlocked;
    }


    public int GetStars(int levelNumber)
    {
        return GameData.LevelData[levelNumber].StarsAwarded;
    }
}
