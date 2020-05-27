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
    //public List <string> userNewName;
    public string userName;
    private string _menuScene = "Menu";
    //[SerializeField] private GameObject _scoreBoard;
    private Scoreboard _sb;
    private void Awake()
    {
       // _sb = _scoreBoard.GetComponent<Scoreboard>();
        
    }


    public void ClickSaveButton()
    {
        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetString(userName, userNameBox.text);
        }
        SceneManager.LoadScene(_menuScene);
        Debug.Log("Your name is: " + PlayerPrefs.GetString(userName));
    }
}