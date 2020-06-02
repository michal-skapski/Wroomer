using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CalcualateScoreboard : MonoBehaviour
{
    [SerializeField] public TMP_Text[] nameBox;
    [SerializeField] public TMP_Text[] scoreAmountBox;
    [SerializeField] private TMP_Text[] _posNumber;
    [SerializeField] public GameObject _menuObjects;
    [SerializeField] public GameObject _scoreboardObjects;
    
    private int _zeroVal = 0;
    private int _sixVal = 6;

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
        for (int _zeroVal = 0; _zeroVal < _sixVal; _zeroVal++)
        {
            if (nameBox[_zeroVal]!= null)
            {
                nameBox[_zeroVal].text = PlayerPrefs.GetString("name");
                scoreAmountBox[_zeroVal].text = PlayerPrefs.GetString("userScore");
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
}
