using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Transform _weapTarget;
    [SerializeField] private Transform _frontTurn;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _speed = 0;
    [SerializeField] private float _fireRate = 15;
    private float _nextTimeToFire = 0f;
    private float _timeMeasure = 1f;
    [SerializeField] private GameObject _gun;
    //roofweapons
    [SerializeField] private bool _roofWeapon = false;
    [SerializeField] private GameObject _tpsCam;
    [SerializeField] private Transform _gunRotate;
    private float mouseY;
    private int _limit = 60;
    [SerializeField] private bool _autoGunControl = false;
    void Update()
    {   //TEST
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + _timeMeasure / _fireRate;
            Shoot();
        }
        if (_autoGunControl == false)
        {
            if (_roofWeapon == true && _gunRotate != null)
            {
                mouseY = Mathf.Clamp(mouseY, -_limit, _limit);
                _gunRotate.transform.rotation = _tpsCam.transform.rotation;
            }
        }
        else if (_autoGunControl == true)
        {
            AutoAim();
        }
    }
    public void JoystickShot()
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + _timeMeasure / _fireRate;
            Shoot();
        }
    }
    void AutoAim()
    {
        if (_weapTarget != null)
        {
            _gunRotate.LookAt(_weapTarget);
        }
        else
        {
            _gunRotate.transform.rotation = _frontTurn.transform.rotation;
        }
    }
    void Shoot()
    { //pooling still being investigated
        if (_gun != null)
        {
            GameObject projectile = Instantiate(_prefab) as GameObject;
            projectile.transform.position = transform.position + _gun.transform.forward;
            projectile.transform.rotation = _gun.transform.rotation;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = _gun.transform.forward * _speed;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        _weapTarget = other.transform;
    }
}
