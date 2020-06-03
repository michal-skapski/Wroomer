using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _thisMenu;
    public static GameObject _thisStaticMenu;
    [SerializeField] private Transform _compass;
    [SerializeField] private Image _clock;
    [SerializeField] private GameObject _clockTimer;
    [SerializeField] private float _timer;
    private float _remainingTime;
    private Animator anim;
    private Animator _2ndanim;
    private Animator _3rdanim;
    [SerializeField] private TMP_Text _scoreCount;
    private int _score; 
    static public int score;
    private int _zeroVal = 0;
    private float _baseTimeMeasure = 1f;
    [SerializeField] private float _secondWave;
    [SerializeField] private float _thirdWave;
    [SerializeField] private GameObject _sndWave;
    [SerializeField] private GameObject _trdWave;
    [SerializeField] private GameObject _sndWaveText;
    [SerializeField] private GameObject _trdWaveText;
    [SerializeField] private bool _inArena = false;
    //menuScenes
    private string _menuScene = "Menu";
    private string _gameScene = "Prototype_01";
    private string _nameAssignerScene = "NameAssigner";
    private void Awake()
    {
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
    }
    private void Start()
    {
        AssignMenu();
        if (_inArena == true)
        {
            _score = _zeroVal;
            score = _score;
            _thisStaticMenu = _thisMenu;
            _remainingTime = _timer;
            StartCoroutine("ArenaMatchStart");
            StartCoroutine("SecondWave");
            StartCoroutine("ThirdWave");
            anim = _clockTimer.GetComponent<Animator>();
            _3rdanim = _trdWaveText.GetComponent<Animator>();
            _2ndanim = _sndWaveText.GetComponent<Animator>();
        }
    }
    void Update()
    {
        if (_inArena == true)
        {
            _remainingTime -= _baseTimeMeasure * Time.deltaTime;
            _clock.fillAmount = _remainingTime / _timer;
        }
    }
    //add listener onclick
    public void GoToMenu()
    {
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(_nameAssignerScene);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(_gameScene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void AssignMenu()
    {
        AltCarMove.menuManager = _thisMenu;
        AltCarMove.otherObject = _compass;
    }
    public void AddScore(int points)
    {
        _score += points;
        score = _score;
        _scoreCount.text = "" + _score;
    }
    IEnumerator SecondWave()
    {
        yield return new WaitForSeconds(_secondWave);
        _sndWave.SetActive(true);
        _2ndanim.SetTrigger("BlowUp");
    }
    IEnumerator ThirdWave()
    {
        yield return new WaitForSeconds(_thirdWave);
        _trdWave.SetActive(true);
        _3rdanim.SetTrigger("BlowUp");
        anim.SetTrigger("BlowUp");
    }
    IEnumerator ArenaMatchStart()
    {
        yield return new WaitForSeconds(_timer);
        GoToMenu();
    }
}
