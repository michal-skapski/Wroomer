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

    private void PinningUserScore()
    {
        _highScore = SystemJson.Instance.HighScore;
        
        for (int _zeroVal = 0; _zeroVal < _sixVal; _zeroVal++)
        {
            if (nameBox[_zeroVal]!= null)
            {
                nameBox[_zeroVal].text = PlayerPrefs.GetString("name");
                scoreAmountBox[_zeroVal].text = _highScore[_zeroVal].ToString();
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
    public void ResetUserScores()
    {
        PlayerPrefs.DeleteAll();
    }
    private void Awake()
    {

        string loadString = File.ReadAllText(Application.dataPath + "/highscores.txt");
        SaveJson loadedSaveObject = JsonUtility.FromJson<SaveJson>(loadString);
        _highScore = loadedSaveObject.bestScores;

        if (PlayerPrefs.GetString("scene") == "NameAssigner")
        {
            _menuObjects.SetActive(false);
            _scoreboardObjects.SetActive(true);
        }
    }
    void Start()
    {
        PinningUserScore();
    }
}