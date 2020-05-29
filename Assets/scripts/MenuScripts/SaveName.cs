using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SaveName : MonoBehaviour
{
    public TMP_InputField textBox;
    private string _menuScene = "Menu";
    private int _userScore;
    public Text[] userNames;
    private CalcualateScoreboard _scoreboardScript;
    private void Awake()
    {
        _scoreboardScript = GetComponent<CalcualateScoreboard>();
        _userScore = Menu.score;
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void ClickSaveButton()
    {
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("name", textBox.text);
        PlayerPrefs.SetString("userScore", _userScore.ToString());
        SceneManager.LoadScene(_menuScene);
    }    
}