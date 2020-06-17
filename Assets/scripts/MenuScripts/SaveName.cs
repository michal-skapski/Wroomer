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
    public int userScore;
    private CalcualateScoreboard _scoreboardScript;
    private int _zeroVal = 0;
    private void Awake()
    {
        _scoreboardScript = GetComponent<CalcualateScoreboard>();
        userScore = Menu.score;
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void ClickSaveButton()
    {
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name); // this string makes transfer directly to scoreboard not to menu
        //PlayerPrefs.SetString("name", textBox.text);
        //PlayerPrefs.SetString("userScore", userScore.ToString());
    }
}