using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CalcualateScoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _nameBox;
    [SerializeField] private TMP_Text[] _scoreAmountBox;
    [SerializeField] public GameObject _menuObjects;
    [SerializeField] public GameObject _scoreboardObjects;
    
    private int _firstScore = 0;

    private void Awake()
    {
        if(PlayerPrefs.GetString("scene")== "NameAssigner")
        {
           _menuObjects.SetActive(false);
           _scoreboardObjects.SetActive(true);
        }
    }

    void Start()
    {
        PinningUserScore();
    }
    private void PinningUserScore()
    {
        _nameBox[_firstScore].text = PlayerPrefs.GetString("name");
        _scoreAmountBox[_firstScore].text = PlayerPrefs.GetString("userScore");
    }
    public void ResetUserScores()
    {
        PlayerPrefs.DeleteAll();
    }
}
