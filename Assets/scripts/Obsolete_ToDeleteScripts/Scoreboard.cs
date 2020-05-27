using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{

    public int gameNum;
    public int pointScore;
    private int _gameNum;
    private int _pointScore;
    public List <GameObject> _posNumber;
    public List <GameObject> _name;
    public List <GameObject> _score;
    private int _zeroVal = 0;
    [SerializeField] private int _minScore = 0;
    public List <TextMeshProUGUI> _nameText;
    [SerializeField] private List <TMP_Text> _scoreCount;
    [SerializeField] private GameObject _nameAssignerObject;
    private NameAssignerScene _nameAssigner;
    

    private void Awake()
    {
        
        gameNum = Random.Range(_zeroVal, _posNumber.Count);
        _pointScore = Menu.score;
        pointScore = _pointScore;
        if (_pointScore > _minScore)
        {
            _gameNum = gameNum;
            _nameAssigner = _nameAssignerObject.GetComponent<NameAssignerScene>();
            for (int i = 0; i < _posNumber.Count; i++)
            {
                _posNumber[i].SetActive(false);
            }
            for (int i = 0; i < _name.Count; i++)
            {
                _name[i].SetActive(false);
            }
            for (int i = 0; i < _score.Count; i++)
            {
                _score[i].SetActive(false);
            }
            AddingValuesToScore();
        }
    }
    public void AddingValuesToScore()
    {
        AddingNewScore(gameNum);
        //nameText[gameNum].text = _nameAssigner.userNewName.ToString();
        //TMP_Text text;
        //text = _name[gameNum].GetComponent<Text>().text;
        //text.text = "" + NameAssignerScene.playerName;
        _nameText[gameNum].text = "Paczajta" + NameAssignerScene.playerName; //NameAssignerScene.playerName.GetComponent<Text>().text;
        _scoreCount[gameNum].text = "" + _pointScore;
    }
    public void AddingNewScore(int gameNum)
    {
        _posNumber[gameNum].SetActive(true);
        _name[gameNum].SetActive(true);
        _score[gameNum].SetActive(true);
        /*_gameNum = gameNum;
        _gameNum--;
        for (int i = _gameNum; i < gameNum; i++) // add later for assigning score place
        {
            _posNumber[i].SetActive(true);
            _name[i].SetActive(true);
            _score[i].SetActive(true);
        }
        */
    }
}
