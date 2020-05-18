using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
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
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + _timeMeasure / _fireRate;
            Shoot();
        }
        if(_roofWeapon == true && _gunRotate != null)
        {
            mouseY = Mathf.Clamp(mouseY, -_limit, _limit);
            _gunRotate.transform.rotation = _tpsCam.transform.rotation;
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
}
