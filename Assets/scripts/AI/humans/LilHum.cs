using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilHum : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    private Animator anim;
    [SerializeField] private GameObject _thisAI;
    [SerializeField] private Transform _thisGuy;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxSpeed = 200f;
    [SerializeField] private float _rotSpeed = 0.05f;
    [SerializeField] private int _distToNotice = 20;
    private bool _destroyed = false;
    private bool _gaveScore = false; // for substracting score purpose
    [SerializeField] private int _points = -1;
    [SerializeField] private int _pointsIfEnemy = 3;
    [SerializeField] private bool _isEnemy = false;
    private float _timeToDie = 5f;
    private int _zeroVal = 0;
    //gun part
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _bulletSpeed = 0;
    [SerializeField] private float _fireRate = 15;
    private float _nextTimeToFire = 0f;
    private float _timeMeasure = 1f;
    [SerializeField] private GameObject _gun;
    //menu adjusting part
    private GameObject _menu;
    private Menu _menuRef;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _menu = Menu._thisStaticMenu;
        _menuRef = _menu.GetComponent<Menu>(); 
    }
    void Update()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, _maxSpeed); //fix //dont know how however
        if (_destroyed == false)
        {
            if (_isEnemy == false)
            {
                Roam();
            }
            if (_isEnemy == true)
            {
                Aim();
            }
        }
    }
    private void Roam()
    {
        if (target != null)
        {
            Vector3 direction = target.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            if (Vector3.Distance(target.position, this.transform.position) < _distToNotice)// && angle < visionAngle
            {
                direction.y = _zeroVal;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                    Quaternion.LookRotation(-direction), _rotSpeed);
                    Move();
            }
        }
    }
    private void Move()
    {
        rb.AddForce(_thisAI.transform.forward * _speed * Time.deltaTime, ForceMode.Acceleration);
    }
    private void Aim()
    {
        if (target != null)
        {
            _thisGuy.LookAt(target);
            if (Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + _timeMeasure / _fireRate;
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        GameObject projectile = Instantiate(_prefab) as GameObject;
        projectile.transform.position = _gun.transform.position + _gun.transform.forward;
        projectile.transform.rotation = _gun.transform.rotation;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = _gun.transform.forward * _bulletSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isEnemy == false)
        {
            target = other.transform;
        }
        if (_isEnemy == true)
        {
            if (other.GetComponent<Car>())
            {
                target = other.transform;
            }
            else
            {
                target = null;
            }
        }
    }
    public void Die()
    {
        _destroyed = true;
        if (_gaveScore == false)
        {
            if (_isEnemy == false)
            {
                _menuRef.AddScore(_points);
                _gaveScore = true;
            }
            if (_isEnemy == true)
            {
                _menuRef.AddScore(_pointsIfEnemy);
                _gaveScore = true;
            }
            StartCoroutine("Vanquish");
        }
    }
    IEnumerator Vanquish()
    {
        yield return new WaitForSeconds(_timeToDie);
        Destroy(_thisAI, _zeroVal);
    }
}
