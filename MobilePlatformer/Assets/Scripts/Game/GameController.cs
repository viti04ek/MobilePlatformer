using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using DG.Tweening;


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

    [HideInInspector] public GameData GameData;
    private string _dataFilePath;
    private BinaryFormatter BinaryFormatter;

    public int CoinValue;
    public int BigCoinValue;
    public int EnemyValue;
    public UI UI;

    public float MaxTime;
    private float _timeLeft;

    public GameObject BigCoin;

    public GameObject Player;
    public GameObject Lever;

    public GameObject EnemySpawner;
    public GameObject SignPlatform;

    private bool _timerOn;

    private bool _isPaused;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        BinaryFormatter = new BinaryFormatter();
        _dataFilePath = Application.persistentDataPath + "/game.dat";
    }


    private void Start()
    {
        DataController.Instance.RefreshData();
        GameData = DataController.Instance.GameData;
        RefreshUI();

        _timeLeft = MaxTime;
        _timerOn = true;
        _isPaused = false;
        HandleFirstBoot();
        UpdateHearts();
        UI.BossHealth.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (_isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        if (_timeLeft > 0 && _timerOn)
            UpdateTimer();
    }


    public void RefreshUI()
    {
        UI.CoinCount.text = $"x {GameData.CoinCount}";
        UI.Score.text = $"Score: {GameData.Score}";
    }


    private void OnEnable()
    {
        Debug.Log("Data loaded");
        RefreshUI();
    }


    private void OnDisable()
    {
        Debug.Log("Data saved");
        DataController.Instance.SaveData(GameData);

        Time.timeScale = 1;
        AdsController.Instance.HideBanner();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        if (GameData.KeyFound[0] && GameData.KeyFound[1])
            ShowSignPlatform();
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
            DataController.Instance.SaveData(GameData);
            Invoke("GameOver", RestartDelay);
        }
        else
        {
            DataController.Instance.SaveData(GameData);
            Invoke("RestartLevel", RestartDelay);
        }
    }


    private void GameOver()
    {
        if (_timerOn)
            _timerOn = false;

        UI.GameOver.SetActive(true);
        UI.GameOver.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);
        AdsController.Instance.ShowBanner();
        DeleteCheckpoint();
    }


    public void BulletHitEnemy(Transform enemy)
    {
        Vector3 position = enemy.position;
        position.z = 20f;
        SFXController.Instance.EnemyExplosion(position);

        Destroy(enemy.gameObject);
        Instantiate(BigCoin, position, Quaternion.identity);

        AudioController.Instance.EnemyExplosion(position);
    }


    public void PlayerStompsEnemy(GameObject enemy)
    {
        enemy.tag = "Untagged";
        Destroy(enemy);
        UpdateScore(Item.Enemy);
    }


    public void StopCameraFollow()
    {
        Camera.main.GetComponent<CameraController>().enabled = false;
        Player.GetComponent<PlayerController>().isStuck = true;
        Player.transform.Find("LeftCheck").gameObject.SetActive(false);
        Player.transform.Find("RightCheck").gameObject.SetActive(false);
    }


    public void ShowLever()
    {
        Lever.SetActive(true);
        SFXController.Instance.ShowPlayerLanding(Lever.transform.position);
        AudioController.Instance.EnemyExplosion(Lever.transform.position);
        DeactivateEnemySpawner();
    }


    public void ActivateEnemySpawner()
    {
        EnemySpawner.SetActive(true);
    }


    public void DeactivateEnemySpawner()
    {
        EnemySpawner.SetActive(false);
    }


    private void ShowSignPlatform()
    {
        SignPlatform.SetActive(true);
        SFXController.Instance.ShowPlayerLanding(SignPlatform.transform.position);
        _timerOn = false;
        UI.BossHealth.gameObject.SetActive(true);
    }


    public int GetScore()
    {
        return GameData.Score;
    }


    public void SetStarsAwarded(int levelNumber, int numberOfStars)
    {
        GameData.LevelData[levelNumber].StarsAwarded = numberOfStars;
    }


    public void UnlockLevel(int levelNumber)
    {
        GameData.LevelData[levelNumber + 1].IsUnlocked = true;
    }


    public void LevelComplete()
    {
        if (_timerOn)
            _timerOn = false;

        UI.MobileUI.SetActive(false);
        UI.LevelCompleteMenu.SetActive(true);
    }


    public void ShowPausePanel()
    {
        UI.Pause.SetActive(true);
        if (UI.MobileUI.activeInHierarchy)
            UI.MobileUI.SetActive(false);

        UI.Pause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);
        AdsController.Instance.ShowBanner();
        Invoke("SetPause", 1.1f);
    }


    private void SetPause()
    {
        _isPaused = true;
    }


    public void HidePausePanel()
    {
        _isPaused = false;
        //UI.Pause.SetActive(false);
        if (!UI.MobileUI.activeInHierarchy)
            UI.MobileUI.SetActive(true);

        UI.Pause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(Screen.height, 0.7f, false);
        AdsController.Instance.HideBanner();
    }


    private void DeleteCheckpoint()
    {
        PlayerPrefs.DeleteKey("CPX");
        PlayerPrefs.DeleteKey("CPY");
    }
}
