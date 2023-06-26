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
    public int Score;
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
        //Score = GameController.Instance.GetScore();
        ScoreText.text = Score.ToString();

        if (Score >= ScoreForThreeStars)
        {
            _showThreeStars = true;
            Invoke("ShowGoldenStars", AnimStartDelay);
        }

        if (Score >= ScoreForTwoStars && Score < ScoreForThreeStars)
        {
            _showTwoStars = true;
            Invoke("ShowGoldenStars", AnimStartDelay);
        }

        if (Score >= ScoreForOneStar && Score != 0)
        {
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
    }


    private IEnumerator HandleSecondStarAnim(Image star)
    {
        DoAnim(star);
        yield return new WaitForSeconds(AnimDelay);

        _showTwoStars = false;
        if (_showThreeStars)
            StartCoroutine("HandleThirdStarAnim", Star3);
    }


    private IEnumerator HandleThirdStarAnim(Image star)
    {
        DoAnim(star);
        yield return new WaitForSeconds(AnimDelay);

        _showThreeStars = false;
    }
}
