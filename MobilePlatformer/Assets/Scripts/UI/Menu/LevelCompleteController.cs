using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelCompleteController : MonoBehaviour
{
    public Button NextLevel;
    public Sprite GoldenStar;
    public Image Star1;
    public Image Star2;
    public Image Star3;
    public Text ScoreText;
    public int LevelNumber;
    [HideInInspector] public int Score;
    public int ScoreForThreeStars;
    public int ScoreForTwoStars;
    public int ScoreForOneStar;
    public int ScoreForNextLevel;
    public float AnimStartDelay;
    public float AnimDelay;

    private bool _showTwoStars;
    private bool _showThreeStars;


    private void Start()
    {
        Score = GameController.Instance.GetScore();
        ScoreText.text = Score.ToString();

        if (Score >= ScoreForThreeStars)
        {
            _showThreeStars = true;
            GameController.Instance.SetStarsAwarded(LevelNumber, 3);
            Invoke("ShowGoldenStars", AnimStartDelay);
        }

        if (Score >= ScoreForTwoStars && Score < ScoreForThreeStars)
        {
            _showTwoStars = true;
            GameController.Instance.SetStarsAwarded(LevelNumber, 2);
            Invoke("ShowGoldenStars", AnimStartDelay);
        }

        if (Score >= ScoreForOneStar && Score < ScoreForTwoStars && Score != 0)
        {
            GameController.Instance.SetStarsAwarded(LevelNumber, 1);
            Invoke("ShowGoldenStars", AnimStartDelay);
        }

    }


    private void ShowGoldenStars()
    {
        StartCoroutine("HandleFirstStarAnim", Star1);
    }


    private void DoAnim(Image star)
    {
        star.rectTransform.sizeDelta = new Vector2(150f, 150f);
        star.sprite = GoldenStar;
        RectTransform t = star.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f), 0.5f, false);
        AudioController.Instance.KeyFound(Camera.main.ScreenToWorldPoint(star.gameObject.transform.position));
        SFXController.Instance.ShowBulletSparkle(Camera.main.ScreenToWorldPoint(star.gameObject.transform.position));
    }


    private IEnumerator HandleFirstStarAnim(Image star)
    {
        DoAnim(star);
        yield return new WaitForSeconds(AnimDelay);

        if (_showTwoStars || _showThreeStars)
            StartCoroutine("HandleSecondStarAnim", Star2);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }


    private IEnumerator HandleSecondStarAnim(Image star)
    {
        DoAnim(star);
        yield return new WaitForSeconds(AnimDelay);

        _showTwoStars = false;
        if (_showThreeStars)
            StartCoroutine("HandleThirdStarAnim", Star3);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }


    private IEnumerator HandleThirdStarAnim(Image star)
    {
        DoAnim(star);
        yield return new WaitForSeconds(AnimDelay);

        _showThreeStars = false;
        Invoke("CheckLevelStatus", 1.2f);
    }


    private void CheckLevelStatus()
    {
        if (Score >= ScoreForNextLevel)
        {
            NextLevel.interactable = true;
            SFXController.Instance.ShowBulletSparkle(Camera.main.ScreenToWorldPoint(NextLevel.gameObject.transform.position));
            AudioController.Instance.KeyFound(Camera.main.ScreenToWorldPoint(NextLevel.gameObject.transform.position));
            GameController.Instance.UnlockLevel(LevelNumber);
        }
        else
        {
            NextLevel.interactable = false;
        }
    }
}
