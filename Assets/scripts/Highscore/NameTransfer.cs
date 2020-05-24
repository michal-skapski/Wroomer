using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameTransfer : MonoBehaviour
{
    [SerializeField] private GameObject _inputField;
    public string userName;
    //public GameObject textDisplay;
    private void Awake()
    {

    }
    public void StoreName()
    {   
        userName = _inputField.GetComponent<TextMeshProUGUI>().text;
        //textDisplay.GetComponent<TextMeshProUGUI>().text = userName; 
    }
}