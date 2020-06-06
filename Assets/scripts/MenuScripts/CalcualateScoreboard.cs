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
    [SerializeField] public GameObject _menuObjects;
    [SerializeField] public GameObject _scoreboardObjects;
    private SaveName _saveNameScript;
    private int[] _highScore = new int[6];
    
    private int _zeroVal = 0;
    private int _sixVal = 6;

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

    private void CheckingScore()
    {
        if (_saveNameScript.userScore > _zeroVal || _saveNameScript.userScore >= _highScore[_zeroVal])
        {
            for (int i = _zeroVal; i < _sixVal; i++)
            {
                PinningUserScore();
                SelectionSort(_highScore);
                SystemJSON.Instance.Save();
            }

        }
    }

    private void PinningUserScore()
    {
        _highScore = SystemJSON.Instance.Highscore;
        
        for (int i = 0; _zeroVal < _sixVal; i++)
        {
            if (nameBox[i]!= null)
            {
                nameBox[i].text = PlayerPrefs.GetString("name");
                scoreAmountBox[i].text = _highScore[_zeroVal].ToString();
                //scoreAmountBox[_zeroVal].text = PlayerPrefs.GetString("userScore");
            }
            else
            {
                //nameBox[_zeroVal].text = PlayerPrefs.GetString("name");
                //scoreAmountBox[_zeroVal].text = PlayerPrefs.GetString("userScore");

            }
        }
        
        //nameBox[zeroVal].text = PlayerPrefs.GetString("name");
        //scoreAmountBox[zeroVal].text = PlayerPrefs.GetString("userScore");
    }
    public void ResetUserScores() // do it by the json method 
    {
        //PlayerPrefs.DeleteAll();
    }
    private void Awake()
    {
        _saveNameScript = GetComponent<SaveName>();
        SystemJSON.Instance.Load();

        if (PlayerPrefs.GetString("scene") == "NameAssigner")
        {
            _menuObjects.SetActive(false);
            _scoreboardObjects.SetActive(true);
        }
    }
    void Start()
    {
        CheckingScore();
    }
}