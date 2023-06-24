using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


public enum Item
{
    Coin,
    BigCoin,
    Enemy
}


public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float RestartDelay;

    public GameData GameData;
    private string _dataFilePath;
    private BinaryFormatter BinaryFormatter;

    public int CoinValue;
    public int BigCoinValue;
    public int EnemyValue;
    public UI UI;

    public float MaxTime;
    private float _timeLeft;

    public GameObject BigCoin;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        BinaryFormatter = new BinaryFormatter();
        _dataFilePath = Application.persistentDataPath + "/game.dat";
    }


    private void Start()
    {
        _timeLeft = MaxTime;

        HandleFirstBoot();
        UpdateHearts();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ResetData();

        if (_timeLeft > 0)
            UpdateTimer();
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
        GameData.Lives = 3;
        UpdateHearts();

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
        CheckLives();
    }


    public void PlayerDiedAnimation(GameObject player)
    {
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(new Vector2(-150f, 400f));

        player.transform.Rotate(new Vector3(0, 0, 45));

        player.GetComponent<PlayerController>().enabled = false;

        foreach (var c2d in player.transform.GetComponents<Collider2D>())
        {
            c2d.enabled = false;
        }

        foreach (Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }

        Camera.main.GetComponent<CameraController>().enabled = false;
        rigidbody2D.velocity = Vector2.zero;

        StartCoroutine("PauseBeforeReload", player);
    }


    IEnumerator PauseBeforeReload(GameObject player)
    {
        yield return new WaitForSeconds(1.5f);
        PlayerDied(player);
    }


    public void PlayerDrowned()
    {
        CheckLives();
    }


    private void RestartLevel()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void UpdateCointCount()
    {
        GameData.CoinCount++;
        UI.CoinCount.text = $"x {GameData.CoinCount}";
    }


    public void UpdateScore(Item item)
    {
        int itemValue = 0;
        switch (item)
        {
            case Item.BigCoin:
                itemValue = BigCoinValue;
                break;

            case Item.Coin:
                itemValue = CoinValue;
                break;

            case Item.Enemy:
                itemValue = EnemyValue;
                break;
        }

        GameData.Score += itemValue;
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


    private void UpdateTimer()
    {
        _timeLeft -= Time.deltaTime;
        UI.Timer.text = $"Timer: {(int)_timeLeft}";

        if (_timeLeft <= 0)
        {
            UI.Timer.text = "Timer: 0";

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerDied(player);
        }
    }


    private void HandleFirstBoot()
    {
        if (GameData.IsFirstBoot)
        {
            GameData.Lives = 3;
            GameData.CoinCount = 0;
            GameData.Score = 0;
            GameData.KeyFound[0] = false;
            GameData.KeyFound[1] = false;
            GameData.KeyFound[2] = false;
            GameData.IsFirstBoot = false;
        }
    }


    private void UpdateHearts()
    {
        if (GameData.Lives == 3)
        {
            UI.Heart1.sprite = UI.FullHeart;
            UI.Heart2.sprite = UI.FullHeart;
            UI.Heart3.sprite = UI.FullHeart;
        }

        if (GameData.Lives == 2)
        {
            UI.Heart1.sprite = UI.EmptyHeart;
        }

        if (GameData.Lives == 1)
        {
            UI.Heart1.sprite = UI.EmptyHeart;
            UI.Heart2.sprite = UI.EmptyHeart;
        }
    }


    private void CheckLives()
    {
        int updatedLives = GameData.Lives;
        updatedLives--;
        GameData.Lives = updatedLives;

        if (GameData.Lives == 0)
        {
            GameData.Lives = 3;
            SaveData();
            Invoke("GameOver", RestartDelay);
        }
        else
        {
            SaveData();
            Invoke("RestartLevel", RestartDelay);
        }
    }


    private void GameOver()
    {
        UI.GameOver.SetActive(true);
    }


    public void BulletHitEnemy(Transform enemy)
    {
        Vector3 position = enemy.position;
        position.z = 20f;
        SFXController.Instance.EnemyExplosion(position);

        Destroy(enemy.gameObject);
        Instantiate(BigCoin, position, Quaternion.identity);

    }


    public void PlayerStompsEnemy(GameObject enemy)
    {
        enemy.tag = "Untagged";
        Destroy(enemy);
        UpdateScore(Item.Enemy);
    }
}
