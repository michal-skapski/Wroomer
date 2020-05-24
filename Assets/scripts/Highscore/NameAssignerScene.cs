using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NameAssignerScene : MonoBehaviour
{
    public TMP_InputField userNameBox;
    public string userNewName;
    private string _menuScene = "Menu";

    private void Start()
    {
        GetName();
    }


    public void GetName()
    {

    }

    public void ClickSaveButton()
    {
        PlayerPrefs.SetString(userNewName, userNameBox.text);
        SceneManager.LoadScene(_menuScene);
        Debug.Log("Your name is: " + PlayerPrefs.GetString(userNewName));
    }
}