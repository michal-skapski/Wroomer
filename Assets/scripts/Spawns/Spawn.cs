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

    private void Start()
    {
        Spawning(); // sets off first batch of spawn and whole script in motion
    }
    private void Update()
    {
        if (_prefab == null)
        {
            if (_appearing == false)
            {
                StartCoroutine(StartSpawning()); //sets internal timer in motion
            }
        }
    }
    void Spawning()
    {
        if (_prefab == null)
        {
            _currTimer = _respawnTimer; //resets time
            _prefab = Instantiate(_myPrefab) as GameObject;
            _prefab.transform.position = _spawn.position;
            _prefab.transform.rotation = _spawn.rotation;
                //Debug.Log("Spawned!" + _myName);
                if (_isPlayer == true)
                {
                    _menu.GetComponent<Menu>().AssignMenu();
                }
        }
    }
    IEnumerator StartSpawning()
    {
        _appearing = true;
        yield return new WaitForSeconds(_currTimer); //waits for internal time
        if (_appearing == true)
        {
            _appearing = false;
            Spawning();
        }
    }
}

