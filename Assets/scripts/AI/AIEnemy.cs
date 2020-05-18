using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public Transform player;
    public Transform target;
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private string _myName;
    [SerializeField] private float _rotSpeed = 0.05f;
    [SerializeField] private int _distToNotice = 20;
    [SerializeField] private int _visionAngle = 90;
    [SerializeField] private float _distForAtk = 0.1f;
    [SerializeField] private float _distForCheck = 1f;
    [SerializeField] private float _maxSpeed = 200f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _inRange = false;
    [SerializeField] private bool _attack = false;
    //[SerializeField] private GameObject _hitZone; // add later
    [SerializeField] private GameObject _thisAI;
    [SerializeField] private float _timeToForget = 15f;
    private bool _destroyed = false;
    public bool seesCar = false;
    //gunPart
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _bulletSpeed = 0;
    [SerializeField] private float _fireRate = 15;
    private int _zeroDir = 0;
    private float _nextTimeToFire = 0f;
    private float _timeMeasure = 1f;
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _hpShell;
    private HP _myHp;
    private bool _gaveScore = false; // for adding score purpose
    [SerializeField] private int points = 5;
    private GameObject _menu;
    private Menu _menuRef;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _myHp = _hpShell.GetComponent<HP>();
        _menu = Menu._thisStaticMenu;
        _menuRef = _menu.GetComponent<Menu>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, _maxSpeed);
        if (_destroyed == false)
        {
            if (seesCar == false)
            {
                Roam();
            }
            else if (seesCar == true)
            {
                CarSeen();
            }
        }
        if (seesCar == true && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + _timeMeasure / _fireRate;
            Shoot();
        }
    }
    public void Roam()
    {
        if (target != null)
        {
            Vector3 direction = target.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            direction.y = _zeroDir;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), _rotSpeed);
            Move();
        }
    }
    private void CarSeen()
    {
        if (player != null)
        {
            Vector3 direction = player.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            if (Vector3.Distance(player.position, this.transform.position) < _distToNotice)// && angle < visionAngle
            {
                direction.y = _zeroDir;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                    Quaternion.LookRotation(direction), _rotSpeed);
                if (direction.magnitude > _distForAtk)
                {
                    Move();
                }
            }
        }
        else if (player == null)
        {
            seesCar = false;
        }
    }
    private void Move()
    {
        _attack = false;
        _inRange = true;
        rb.AddForce(_thisAI.transform.forward *_speed * Time.deltaTime, ForceMode.Acceleration);
    }
    public void Hit()
    {
    //    _hitZone.SetActive(true); //will be added later, when we add spikes to cars
    }
    private void Attack()
    {
        _attack = true;
    }
    void Shoot() 
    {
        GameObject projectile = Instantiate(_prefab) as GameObject;
        projectile.transform.position = _gun.transform.position + _gun.transform.forward;
        projectile.transform.rotation = _gun.transform.rotation;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = _gun.transform.forward * _bulletSpeed;
    }
    public void Die()
    {
        _destroyed = true;
        if (_gaveScore == false)
        {
            _menuRef.AddScore(points);
            _gaveScore = true;
        }
    }
    public void DeathByTrail(int dmg)
    {
        _myHp.TakeDamage(dmg);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Car>())
        {
             seesCar = true;
             player = other.transform;
        }
        else
        {
             target = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Car>())
        {
            StartCoroutine("Forget");
        }
    }
    IEnumerator Forget()
    {
        yield return new WaitForSeconds(_timeToForget);
        {
            if(seesCar == true)
            {
                seesCar = false;
            }
        }
    }
}

