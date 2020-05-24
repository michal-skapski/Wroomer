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
    [SerializeField] private TMP_Text _scoreCount;
    private int _score;
    private float _baseTimeMeasure = 1f;
    [SerializeField] private float _secondWave;
    [SerializeField] private float _thirdWave;
    [SerializeField] private GameObject _sndWave;
    [SerializeField] private GameObject _trdWave;
    [SerializeField] private bool _inArena = false;

    private string _menuScene = "Menu";
    private string _prototypeScene = "Prototype_01";
    private void Start()
    {
        AssignMenu();
        if (_inArena == true)
        {
            _thisStaticMenu = _thisMenu;
            _remainingTime = _timer;
            StartCoroutine("ArenaMatchStart");
            StartCoroutine("SecondWave");
            StartCoroutine("ThirdWave");
            anim = _clockTimer.GetComponent<Animator>();
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
        SceneManager.LoadScene(_menuScene);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(_prototypeScene);
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
        _scoreCount.text = "" + _score;
    }
    IEnumerator SecondWave()
    {
        yield return new WaitForSeconds(_secondWave);
        _sndWave.SetActive(true);
    }
    IEnumerator ThirdWave()
    {
        yield return new WaitForSeconds(_thirdWave);
        _trdWave.SetActive(true);
        anim.SetTrigger("BlowUp");
    }
    IEnumerator ArenaMatchStart()
    {
        yield return new WaitForSeconds(_timer);
        GoToMenu();
    }
}
