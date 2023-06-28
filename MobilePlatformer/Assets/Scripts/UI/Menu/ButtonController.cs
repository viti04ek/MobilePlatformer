using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private int _levelNumber;
    private Button _button;
    private Image _buttonImage;
    private Text _buttonText;
    private Transform _star1;
    private Transform _star2;
    private Transform _star3;

    public Sprite LockedButton;
    public Sprite UnlockedButton;
    public string SceneName;


    private void Start()
    {
        _levelNumber = int.Parse(transform.gameObject.name);
        _button = transform.gameObject.GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();
        _buttonText = _button.gameObject.transform.GetChild(0).GetComponent<Text>();
        _star1 = _button.gameObject.transform.GetChild(1);
        _star2 = _button.gameObject.transform.GetChild(2);
        _star3 = _button.gameObject.transform.GetChild(3);

        ButtonStatus();
    }


    private void ButtonStatus()
    {
        bool unlocked = DataController.Instance.IsUnlocked(_levelNumber);
        int starsAwarded = DataController.Instance.GetStars(_levelNumber);

        if (unlocked)
        {
            if (starsAwarded == 3)
            {
                _star1.gameObject.SetActive(true);
                _star2.gameObject.SetActive(true);
                _star3.gameObject.SetActive(true);
            }
            if (starsAwarded == 2)
            {
                _star1.gameObject.SetActive(true);
                _star2.gameObject.SetActive(true);
                _star3.gameObject.SetActive(false);
            }
            if (starsAwarded == 1)
            {
                _star1.gameObject.SetActive(true);
                _star2.gameObject.SetActive(false);
                _star3.gameObject.SetActive(false);
            }
            if (starsAwarded == 0)
            {
                _star1.gameObject.SetActive(false);
                _star2.gameObject.SetActive(false);
                _star3.gameObject.SetActive(false);
            }

            _button.onClick.AddListener(LoadScene);
        }
        else
        {
            _buttonImage.overrideSprite = LockedButton;
            _buttonText.text = "";
            _star1.gameObject.SetActive(false);
            _star2.gameObject.SetActive(false);
            _star3.gameObject.SetActive(false);
        }
    }


    private void LoadScene()
    {
        LoadingController.Instance.ShowLoading();
        SceneManager.LoadScene(SceneName);
    }
}
