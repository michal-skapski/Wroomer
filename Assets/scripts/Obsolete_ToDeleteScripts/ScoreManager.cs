using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    public int newScore;

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Score_Manager");
                go.AddComponent<ScoreManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void AddScore()
    {
        newScore++;
    }
}
