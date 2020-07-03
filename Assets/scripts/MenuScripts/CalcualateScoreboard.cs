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
    private string[] _bestNames = new string[6];
    private int _zeroVal = 0;
    private int _oneVal = 1;
    private int _twoVal = 2;
    private int _threeVal = 3;
    private int _fourVal = 4;
    private int _fiveVal = 5;
    private int _sixVal = 6;
    private string _emptySign = "";
    
    private void CheckingScore()
    {
        PinningUserScore();
    }
    


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

                for (int z = _fiveVal; z >= i; z--)
                {
                    Debug.Log("test");
                    _highScore[z] = _highScore[z - 1];
                    Debug.Log("z: " + z + " z-1: " + (z - 1));
                }
                _highScore[i] = PlayerPrefs.GetInt("currentScore");
                switch (i)
                {
                    case 0:
                        _bestNames[_zeroVal] = PlayerPrefs.GetString("name");
                        break;
                    case 1:
                        _bestNames[_oneVal] = PlayerPrefs.GetString("name");
                        break;
                    case 2:
                        _bestNames[_twoVal] = PlayerPrefs.GetString("name");
                        break;
                    case 3:
                        _bestNames[_threeVal] = PlayerPrefs.GetString("name");
                        break;
                    case 4:
                        _bestNames[_fourVal] = PlayerPrefs.GetString("name");
                        break;
                    case 5:
                        _bestNames[_fiveVal] = PlayerPrefs.GetString("name");
                        break;
                }
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
        CheckingScore();
    }
    private void Update()
    {
        MatchingScore();
    }
}