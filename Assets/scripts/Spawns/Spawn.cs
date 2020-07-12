using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private string _myName;
    [SerializeField] private GameObject _myPrefab;
    private GameObject _prefab;
    [SerializeField] private float _respawnTimer = 0f;
    private float _currTimer;
    public bool _appearing = false;
    [SerializeField] private bool _isPlayer = false;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _deathCam;
    [SerializeField] private bool _changeable = false;
    [SerializeField] private List<Transform> _changeableLocations;
    private int _spawnLoc;

    private void Start()
    {
        Spawning(_spawnLoc); // sets off first batch of spawn and whole script in motion
    }
    private void Update()
    {
        if (_prefab == null)
        {
            if (_appearing == false)
            {
                StartCoroutine(StartSpawning());
                if (_isPlayer == true)
                {
                    _menu.GetComponent<Menu>().AssignMenu();
                } //sets internal timer in motion
            }
        }
    }
    void Spawning(int pos)
    {
        if (_prefab == null)
        {
            _currTimer = _respawnTimer; //resets time
            _prefab = Instantiate(_myPrefab) as GameObject;
            if (_changeable == false)
            {
                _prefab.transform.position = _spawn.position;
                _prefab.transform.rotation = _spawn.rotation;
            }
            else if (_changeable == true)
            {
                _prefab.transform.position = _changeableLocations[pos].position;
                _prefab.transform.rotation = _changeableLocations[pos].rotation;
            }
            //Debug.Log("Spawned!" + _myName);
            if (_isPlayer == true)
            {
                _deathCam.SetActive(false);
                _menu.GetComponent<Menu>().AssignMenu();
            }
        }
    }
    IEnumerator StartSpawning()
    {
        _appearing = true;
        if (_isPlayer == true)
        {
            _deathCam.SetActive(true);
        }
        yield return new WaitForSeconds(_currTimer); //waits for internal time
        if (_appearing == true)
        {
            _appearing = false;
            _spawnLoc = Random.Range(0, _changeableLocations.Count);
            Spawning(_spawnLoc);
        }
    }
}

