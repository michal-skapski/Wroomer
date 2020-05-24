using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NameAssignerScene : MonoBehaviour
{
    public TMP_InputField userNameBox;
    public string[] userNewName;
    private string _menuScene = "Menu";
    private Scoreboard _sb;
    private void Awake()
    {
        _sb.GetComponent<Scoreboard>();
    }


    private void Start()
    {
        GetName();
    }


    public void GetName()
    {

    }

    private void Update()
    {
        ClickSaveButton(); 
        for (int i = 0; i < _sb.gameNumber; i++)
        {
            PlayerPrefs.SetString(userNewName[_sb.gameNumber], userNameBox.text);
        }
    }

    public void ClickSaveButton()
    {
        SceneManager.LoadScene(_menuScene);

;
        //Debug.Log("Your name is: " + PlayerPrefs.GetString(userNewName));
    }
}