using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;

// 1. W tym skrypcie pobrać tablice z systemjson na awaku 
// 2. W tym skrypcie jak gracz skończy grę to sprawdzić czy jego wynik nadaje się do zapisania w tablicy jak tak to podmienić wyniki
// 3. Gdy tablica jest uporządkowana to dopiero wtedy robię zapis całej tablicy
// 4. Najpierw tylko punkty potem imiona  (też jsonem)
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

    private int _zeroVal = 0;
    private int _sixVal = 5;
    
    private void CheckingScore()
    {
        PinningUserScore();
    }
    


    public void SaveArrayScore()
    {

        // tu przechwyć aktualny wynik zz player prefs (current score) - done
        // sprawdź czy zapisać wynik do highscore - czy wynik jest wysoki - done 
        // tak zapisz na odpowiednim miejsu nie zignorować (_highcore)
        // later instantiate - done

        _highScore = SystemJSON.Instance.Highscore;

        for (int i = _zeroVal; i < _sixVal; i++)
        {
            if (PlayerPrefs.GetInt("currentScore") > _highScore[i])
            {

                for (int z = _sixVal; z > i; z--)
                {
                    Debug.Log("test");
                    _highScore[z] = _highScore[z - 1];
                    Debug.Log("z: " + z + " z-1: " + (z - 1));
                }
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
    }
    private void MatchingScore()
    {
        for (int i = 0; i <= _sixVal; i++)
        {
            if (_highScore[i] != _zeroVal)
            {
                _posNumber[i].gameObject.SetActive(true);
                scoreAmountBox[i].text = _highScore[i].ToString();
            }
        }
    }
    public void ResetUserScores() // do it by the json method 
    {
        for (int i = _zeroVal; i < _sixVal; i++)
        {
            _highScore[i] = 0;
        }
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
        CheckingScore();
    }
    private void Update()
    {
        MatchingScore();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("currentScore", 10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("currentScore", 20);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("currentScore", 30);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.SetInt("currentScore", 0);
            for (int i = _sixVal; i < _sixVal; i++)
            {
                _highScore[i] = 0;
            }
            SystemJSON.Instance.Highscore = _highScore;
        }

    }
}