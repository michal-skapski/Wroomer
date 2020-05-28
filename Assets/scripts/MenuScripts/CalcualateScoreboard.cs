using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CalcualateScoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _nameBox;
    [SerializeField] private TMP_Text[] _scoreAmountBox;
    private int _firstScore = 0;
    private int _userScore;
    private void Awake()
    {
        _userScore = Menu.score;
        PlayerPrefs.SetString("userScore", _userScore.ToString());
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
        /*
        PlayerPrefs.DeleteKey("name");
        PlayerPrefs.DeleteKey("userScore");
        */
    }
}
