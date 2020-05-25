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
    public List <string> userNewName;
    private string _menuScene = "Menu";
    [SerializeField] private GameObject _scoreBoard;
    private Scoreboard _sb;
    private void Awake()
    {
        _sb = _scoreBoard.GetComponent<Scoreboard>();
        GetName();
    }
    private void Update()
    {
        for (int i = 0; i < _sb.gameNum; i++)
        {
          //  PlayerPrefs.SetString(userNewName[_sb.gameNum], userNameBox.text);
        }
    }
    public void GetName()
    {
        //add code
    }
    public void ClickSaveButton()
    {
        SceneManager.LoadScene(_menuScene);
        //Debug.Log("Your name is: " + PlayerPrefs.GetString(userNewName));
    }
}