using System.Collections;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreboardManager : MonoBehaviour
{
    private static ScoreboardManager _instance;

    [SerializeField] private GameObject _scoreTemplate;
    [SerializeField] private GameObject[] _scoreAmount;
    [SerializeField] private GameObject[] _userName;
    private Vector3 _scoreEntryPos;
    private Vector3 _newScorePos;
    private void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        ScoreAdding();
    }
    public static ScoreboardManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Scoreboard_Manager");
                go.AddComponent<ScoreboardManager>();
            }
            return _instance;
        }
    }
    public void ScoreAdding()
    {
        _scoreEntryPos = new Vector3(66, 53, 0);
        _newScorePos = new Vector3(10, 10, 0);
        for (int i = 0; i < 10; i++)
        {
            Instantiate(_scoreTemplate, _scoreEntryPos+_newScorePos, Quaternion.identity);
        }
    }

}
