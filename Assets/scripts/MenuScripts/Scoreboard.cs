using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public int gameNumber;
    public GameObject[] _posNumberArray;
    public GameObject[] _nameArray;
    public GameObject[] _scoreArray;
    public TextMeshProUGUI[] nameArrayText;
    private NameAssignerScene _name;


    private void Awake()
    {
        
        _name.GetComponent<NameAssignerScene>();
        _posNumberArray[0].SetActive(false);
        _posNumberArray[1].SetActive(false);
        _posNumberArray[2].SetActive(false);
        _posNumberArray[3].SetActive(false);
        _posNumberArray[4].SetActive(false);
        _posNumberArray[5].SetActive(false);
        _nameArray[0].SetActive(false);
        _nameArray[1].SetActive(false);
        _nameArray[2].SetActive(false);
        _nameArray[3].SetActive(false);
        _nameArray[4].SetActive(false);
        _nameArray[5].SetActive(false);
        _scoreArray[0].SetActive(false);
        _scoreArray[1].SetActive(false);
        _scoreArray[2].SetActive(false);
        _scoreArray[3].SetActive(false);
        _scoreArray[4].SetActive(false);
        _scoreArray[5].SetActive(false);
    }

    public void AddingNewScore()
    {
        switch (gameNumber)
        {
            case 1:
                _posNumberArray[0].SetActive(true);
                _nameArray[0].SetActive(true);
                _scoreArray[0].SetActive(true);
                break;
            case 2:
                _posNumberArray[0].SetActive(true);
                _nameArray[0].SetActive(true);
                _scoreArray[0].SetActive(true);
                _posNumberArray[1].SetActive(true);
                _nameArray[1].SetActive(true);
                _scoreArray[1].SetActive(true);
                break;
            case 3:
                _posNumberArray[0].SetActive(true);
                _nameArray[0].SetActive(true);
                _scoreArray[0].SetActive(true);
                _posNumberArray[1].SetActive(true);
                _nameArray[1].SetActive(true);
                _scoreArray[1].SetActive(true);
                _posNumberArray[2].SetActive(true);
                _nameArray[2].SetActive(true);
                _scoreArray[2].SetActive(true);
                break;
            case 4:
                _posNumberArray[0].SetActive(true);
                _nameArray[0].SetActive(true);
                _scoreArray[0].SetActive(true);
                _posNumberArray[1].SetActive(true);
                _nameArray[1].SetActive(true);
                _scoreArray[1].SetActive(true);
                _posNumberArray[2].SetActive(true);
                _nameArray[2].SetActive(true);
                _scoreArray[2].SetActive(true);
                _posNumberArray[3].SetActive(true);
                _nameArray[3].SetActive(true);
                _scoreArray[3].SetActive(true);
                break;
            case 5:
                _posNumberArray[0].SetActive(true);
                _nameArray[0].SetActive(true);
                _scoreArray[0].SetActive(true);
                _posNumberArray[1].SetActive(true);
                _nameArray[1].SetActive(true);
                _scoreArray[1].SetActive(true);
                _posNumberArray[2].SetActive(true);
                _nameArray[2].SetActive(true);
                _scoreArray[2].SetActive(true);
                _posNumberArray[3].SetActive(true);
                _nameArray[3].SetActive(true);
                _scoreArray[3].SetActive(true);
                _posNumberArray[4].SetActive(true);
                _nameArray[4].SetActive(true);
                _scoreArray[4].SetActive(true);
                break;
            case 6:
                _posNumberArray[0].SetActive(true);
                _nameArray[0].SetActive(true);
                _scoreArray[0].SetActive(true);
                _posNumberArray[1].SetActive(true);
                _nameArray[1].SetActive(true);
                _scoreArray[1].SetActive(true);
                _posNumberArray[2].SetActive(true);
                _nameArray[2].SetActive(true);
                _scoreArray[2].SetActive(true);
                _posNumberArray[3].SetActive(true);
                _nameArray[3].SetActive(true);
                _scoreArray[3].SetActive(true);
                _posNumberArray[4].SetActive(true);
                _nameArray[4].SetActive(true);
                _scoreArray[4].SetActive(true);
                _posNumberArray[5].SetActive(true);
                _nameArray[5].SetActive(true);
                _scoreArray[5].SetActive(true);
                break;
        }
    }

    public void AddingValuesToScore()
    {
        nameArrayText[gameNumber].text = _name.userNewName.ToString();
    }


    private void Update()
    {
        AddingNewScore();
        AddingValuesToScore();
    }
}
