using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HigscoreTableManager : MonoBehaviour
{
    private static HigscoreTableManager _instance;
    [SerializeField] private RectTransform _scoreEntryContainter;
    [SerializeField] private RectTransform _scoreEntryTemplate;
    [SerializeField] private GameObject _posObject;
    [SerializeField] private GameObject _scoreAmountObject;
    private int _min = 1;
    private int _max = 10;
    private float _newPosTemplate = 186.2f;
    private float _templateHeight = 60f;
    private float _entriesDistance = 25f;
    private int _rankNumber;
    private string _rankString;
    public static HigscoreTableManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("HighscoreTableManager");
                go.AddComponent<HigscoreTableManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        _scoreEntryTemplate.gameObject.SetActive(false);
        InstantiateScoreTemplate();
    }
    public void InstantiateScoreTemplate()
    {
        for (int i = _min; i < _max; i++)
        {
            Transform entryTransform = Instantiate(_scoreEntryTemplate, _scoreEntryContainter);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(_newPosTemplate, _templateHeight - _entriesDistance * i);
            _scoreEntryTemplate.gameObject.SetActive(true);
            _rankNumber = i + _min;
            switch (_rankNumber)
            {
                default: _rankString = _rankNumber + "TH"; break;
                case 1: _rankString = "1st"; break;
                case 2: _rankString = "2nd"; break;
                case 3: _rankString = "3rd"; break;
                case 4: _rankString = "4th"; break;
                case 5: _rankString = "5th"; break;
                case 6: _rankString = "6th"; break;
                case 7: _rankString = "7th"; break;
                case 8: _rankString = "8th"; break;
                case 9: _rankString = "9th"; break;
                case 10: _rankString = "10th"; break;
            }
            _posObject.GetComponent<TextMeshProUGUI>().text = _rankString;
        }
    }
}
