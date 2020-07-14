using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;


public class CalcualateScoreboard : MonoBehaviour
{
    [SerializeField] public TMP_Text[] nameBox;
    [SerializeField] public TMP_Text[] scoreAmountBox;
    [SerializeField] private TMP_Text[] _posNumber;
    [SerializeField] private GameObject _menuObjects;
    [SerializeField] private GameObject _scoreboardObjects;
    [SerializeField] private GameObject _saveNameObjects;
    private string _gameSceneName = "Prototype_01";
    [SerializeField] private SaveName _saveNameScript;

    private int[] _highScore = new int[6];
    private string[] _bestNames = new string[6];

    private int _zeroVal = 0;
    private int _fiveVal = 5;
    private int _sixVal = 6;
    private string _emptySign = "";
    
    public void SaveArrayScore()
    {

        // tu przechwyć aktualny wynik zz player prefs (current score) - done
        // sprawdź czy zapisać wynik do highscore - czy wynik jest wysoki - done 
        // tak zapisz na odpowiednim miejsu nie zignorować (_highcore) - done
        // later instantiate - done

        _highScore = SystemJSON.Instance.Highscore;
        _bestNames = SystemJSON.Instance.BestPlayers;

        for (int i = _zeroVal; i < _sixVal; i++)
        {
            if (PlayerPrefs.GetInt("currentScore") > _highScore[i])
            {

                for (int z = _fiveVal; z > i; z--)
                {
                    _highScore[z] = _highScore[z - 1];
                    _bestNames[z] = _bestNames[z - 1];
                }
                _bestNames[i] = PlayerPrefs.GetString("name");
                _highScore[i] = PlayerPrefs.GetInt("currentScore");
                break;
            }
        }
        SystemJSON.Instance.Highscore = _highScore;
        MatchingScore();
    }
    private void PinningUserScore()
    {
        _highScore = SystemJSON.Instance.Highscore;
        _bestNames = SystemJSON.Instance.BestPlayers;
    }
    private void MatchingScore()
    {
        for (int i = 0; i <= _fiveVal; i++)
        {
            if (_highScore[i] != _zeroVal)
            {
                _posNumber[i].gameObject.SetActive(true);
                nameBox[i].text = _bestNames[i];
                scoreAmountBox[i].text = _highScore[i].ToString();
            }
        }
    }
    public void ResetUserScores() // this method is connect to "slaughter" button - it deletes  all the scores and names
    {
        for (int i = _zeroVal; i <= _fiveVal; i++)
        {
            _highScore[i] = 0;
            _bestNames[i] = _emptySign;
        }
        SystemJSON.Instance.Save();
    }
    private void Awake()
    {
        if (PlayerPrefs.GetString("scene") == _gameSceneName)
        {
            _menuObjects.SetActive(false);            
            _saveNameObjects.SetActive(true);
        }
        else
        {
            _menuObjects.SetActive(true);
            _saveNameObjects.SetActive(false);
        }
    }
    void Start()
    {
        PinningUserScore();
    }
    private void Update()
    {
        MatchingScore();
    }
}