using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _life = 5;
    private float _hp;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private GameObject _expl;
    private int _zeroVal = 0;
    // For repo / report
    // enemy is cube for the time being, later update required including
    // changing transforms on trigger enter with player transform 
    // remaining consistent upon forgeting player's position after set time
    void Start()
    {
        _hp = _life;
    }
    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if(_hp <= _life)
        {
            _smoke.Play();
        }
        if(_hp <= _zeroVal)
        {
            GameObject burst = Instantiate(_expl) as GameObject;
            burst.transform.position = _enemy.transform.position;
            Destroy(_enemy, _zeroVal);
        }
    }
}
