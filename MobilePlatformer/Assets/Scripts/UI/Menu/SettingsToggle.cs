using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsToggle : MonoBehaviour
{
    public RectTransform FacebookLike;
    public RectTransform TwitterFollow;
    public RectTransform GooglePlus;
    public RectTransform Ratings;
    public float MoveFB;
    public float MoveTF;
    public float MoveGP;
    public float MoveR;
    public float DefaultPosX;
    public float DefaultPosY;
    public float Speed;
    private bool _expanded;


    private void Start()
    {
        _expanded = false;
    }


    public void Toggle()
    {
        if (!_expanded)
        {
            FacebookLike.DOAnchorPosY(MoveFB, Speed, false);
            TwitterFollow.DOAnchorPosY(MoveTF, Speed, false);
            GooglePlus.DOAnchorPosY(MoveGP, Speed, false);
            Ratings.DOAnchorPosY(MoveR, Speed, false);
            _expanded = true;
        }
        else
        {
            FacebookLike.DOAnchorPosY(DefaultPosY, Speed, false);
            TwitterFollow.DOAnchorPosY(DefaultPosY, Speed, false);
            GooglePlus.DOAnchorPosY(DefaultPosY, Speed, false);
            Ratings.DOAnchorPosY(DefaultPosY, Speed, false);
            _expanded = false;
        }
    }
}
