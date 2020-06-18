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
    private int _fiveVal = 5;
    private int _sixVal = 6;


    private void CheckingScore()
    {
        if (_saveNameScript.userScore > _zeroVal || _saveNameScript.userScore >= _highScore[_zeroVal])
        {
            for (int i = _zeroVal; i < _sixVal; i++)
            {
                PinningUserScore();
                //SelectionSort(_highScore);
            }
        }
    }
    public void SaveArrayScore()
    {
        // tu przechwyć aktualny wynik zz player prefs (current score)
        // sprawdź czy zapisać wynik do highscore - czy wynik jest wysoki 
        // tak zapisz na odpowiednim miejsu nie zignorować (_highcore)
        // later instantiate
        for (int i = _zeroVal; i < _fiveVal; i++)
        {
            if (PlayerPrefs.GetInt("currentScore") > _highScore[i])
            {
                _highScore[i] = PlayerPrefs.GetInt("currentScore");                
            }
        }
        /*
        if (PlayerPrefs.GetInt("currentScore")>_highScore[5])
        {
            _highScore[5] = PlayerPrefs.GetInt("currentScore");
        }
        else
        {
            _highScore[1] = PlayerPrefs.GetInt("currentScore");
        }
        */

        
        SystemJSON.Instance.Highscore = _highScore;
    }
    private void PinningUserScore()
    {
        _highScore = SystemJSON.Instance.Highscore;
        
        
        
        //nameBox[zeroVal].text = PlayerPrefs.GetString("name");
        //scoreAmountBox[zeroVal].text = PlayerPrefs.GetString("userScore");
    }
    public void ResetUserScores() // do it by the json method 
    {
        //PlayerPrefs.DeleteAll();
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






    //script on selecting the scores might become usefull later
    
    private void SelectionSort(int[] unsortedList)
    {
        int min;
        int tempSwappingSpace;
        for (int i = 0; i < unsortedList.Length; i++)
        {
            min = i;
            for (int j = i; j < unsortedList.Length; j++)
            {
                min = j;

            }
            if (min != i)
            {
                tempSwappingSpace = unsortedList[i];
                unsortedList[i] = unsortedList[min];
                unsortedList[min] = tempSwappingSpace;
            }
        }
    }
    
}