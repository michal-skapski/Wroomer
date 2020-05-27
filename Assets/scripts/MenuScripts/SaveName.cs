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
    public Text[] userNames;
    private void Awake()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void ClickSaveButton()
    {
        PlayerPrefs.SetString("name", textBox.text);
        SceneManager.LoadScene(_menuScene);
    }    
}