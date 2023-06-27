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
        RefreshData();
    }


    public void SaveData()
    {
        FileStream fileStream = new FileStream(_dataFilePath, FileMode.Create);
        BinaryFormatter.Serialize(fileStream, GameData);
        fileStream.Close();
        Debug.Log("Data saved");
    }
}
