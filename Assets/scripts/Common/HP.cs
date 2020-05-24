using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private GameObject _thisAI;
    [SerializeField] private GameObject _shell;
    [SerializeField] private List<GameObject> _parts;
    [SerializeField] private int _partNumber = 3; // 3 parts
    [SerializeField] private int _life = 5;
    [SerializeField] private int _deathlyNumber = 0;
    [SerializeField] private float _timeToDestroy = 0;
    [SerializeField] private bool _player = false;
    [SerializeField] private bool _civil = false;
    [SerializeField] private bool _crate = false;
    private float _hp;
    private int _divisionOfHP = 2;
    [SerializeField] private int _divisionOfHP4Run = 2;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _expl;
    [SerializeField] private GameObject _shield;
    [SerializeField] private float _shieldedTime = 10f;
    private AltCarMove _car;
    private AIEnemy _carEn; //enemy shortcut
    private LilHum _civilian;
    private bool _shielded = false;
    private void Start()
    {
        _hp = _life;
        _carEn = _thisAI.GetComponent<AIEnemy>();
        _car = _thisAI.GetComponent<AltCarMove>();
        _civilian = _thisAI.GetComponent<LilHum>();
    }
    public void TakeDamage(int damage)
    {
        if (_shielded == false && _crate == false)
        {
            _hp -= damage;
            _partNumber--;
            TakeParts();
            CheckHealth();
            if(_civil == false && _player == false)
            {
                if(_hp < _life / _divisionOfHP4Run)
                {
                    _carEn.StartFleeing(); // makes enemy wanting to try to hide himself from you
                }          
            }
        }
        else if (_crate == true)
        {
            _shell.SetActive(false);
            _expl.Play();
            Destroy(_thisAI, _timeToDestroy);
        }
    }
    private void CheckHealth()
    {
        if (_hp <= _deathlyNumber) // _deathlyNumber = 0
        {
            _shell.SetActive(false);
            _expl.Play();
            if (_player == true) //either is player
            {
                _car.Die();
            }
            else if (_player == false) // or not, and grabs Enemy AI, there will be correction here, further down the road, when teams are introduced
            {
                if (_civil == true)
                {
                    _civilian.Die();
                }
                else if (_civil == false)
                {
                    _carEn.Die(); // enemy
                }
            }
            Destroy(_thisAI, _timeToDestroy);
        }
    }
    private void TakeParts()
    {
        if (_partNumber <= _deathlyNumber) // assures array does not go below 0 (it corresponds with List further down the script)
        {
            _partNumber = _deathlyNumber;
        }
        if (_hp <= _life) //deactivates part of car, cause of sustained damage
        {
            _parts[_partNumber].SetActive(false);
        }
        if (_hp <= _life / _divisionOfHP) // divided _life = "100%" of _hp by 2, meaning "if(_hp goes down below 50%)"
        {
            _smoke.Play();
        }
    }
    public void Shield()
    {
        StartCoroutine("Shielded"); //activates shield system
    }
    IEnumerator Shielded()
    {
        _shielded = true;
        _shield.SetActive(true);
        yield return new WaitForSeconds(_shieldedTime);
        if(_shielded == true)
        {
            _shield.SetActive(false);
            _shielded = false;
        }
    }
}
