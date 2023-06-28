using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public string FacebookURL;
    public string TwitterURL;
    public string GoogleURL;
    public string RatingURL;


    public void FacebookLike()
    {
        Application.OpenURL(FacebookURL);
    }


    public void TwitterFollow()
    {
        Application.OpenURL(TwitterURL);
    }


    public void GooglePlus()
    {
        Application.OpenURL(GoogleURL);
    }


    public void Rating()
    {
        Application.OpenURL(RatingURL);
    }
}
