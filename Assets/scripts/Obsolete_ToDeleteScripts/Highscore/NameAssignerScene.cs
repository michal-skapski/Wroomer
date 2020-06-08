using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.Collections.Generic;

public class NameAssignerScene : MonoBehaviour
{
    public TMP_InputField userNameBox;
    public static Text playerName;
    public string userName;
    private string _menuScene = "Menu";
    private Scoreboard _sb;
    private int _zeroVal = 0;
    private int _maxVal = 6;
    public void ClickSaveButton()
    {
        for (int i = _zeroVal; i < _maxVal; i++)
        {
            PlayerPrefs.SetString(userName, userNameBox.text);
        }
        SceneManager.LoadScene(_menuScene);
        Debug.Log("Your name is: " + PlayerPrefs.GetString(userName));
    }
}