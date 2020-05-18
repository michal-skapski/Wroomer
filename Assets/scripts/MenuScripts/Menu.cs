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
    [SerializeField] private float _timer;
    private float _remainingTime;
    [SerializeField] private TMP_Text _scoreCount;
    private int _score;
    private float _baseTimeMeasure = 1f;
    [SerializeField] private bool _inArena = false;
    private void Start()
    {
        AssignMenu();
        if (_inArena == true)
        {
            _thisStaticMenu = _thisMenu;
            _remainingTime = _timer;
            StartCoroutine("ArenaMatchStart");
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
        SceneManager.LoadScene(0);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
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
    IEnumerator ArenaMatchStart()
    {
        yield return new WaitForSeconds(_timer);
        GoToMenu();
    }
}
