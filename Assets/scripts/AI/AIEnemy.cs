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
    private bool _flee = false;
    private bool _isPlayer = false;
    //rayDetectingPart
    [SerializeField] private GameObject _rayEmmiter;
    public float _interactDist = 0.1f;
    private float _backUp = 1f;
    public bool _isStuck = false;
    //gunPart
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _bulletSpeed = 0;
    [SerializeField] private float _fireRate = 15;
    private int _zeroDir = 0;
    private float _nextTimeToFire = 0f;
    private float _timeMeasure = 1f;
    private bool _hitByTrail = true;
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
            CheckForWall();
            if (seesCar == false)
            {
                Roam();
            }
            if (seesCar == true)
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
                if (_flee == false)
                {
                    direction.y = _zeroDir;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        Quaternion.LookRotation(direction), _rotSpeed);
                }
                else if (_flee == true)
                {
                    direction.y = _zeroDir;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        Quaternion.LookRotation(-direction), _rotSpeed);
                    //CheckForWall();
                }
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
        if (_isStuck == false)
        {
            rb.AddForce(_thisAI.transform.forward * _speed * Time.deltaTime, ForceMode.Acceleration);
        }
        else if(_isStuck == true)
        {
            StartCoroutine(BackUp());
        }
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
    private void CheckForWall() //checks if ground is beneath using short ray to see if "R" click and position restart can be used
    {
        Ray ray = new Ray(_rayEmmiter.transform.position, _rayEmmiter.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _interactDist))
        {
            Debug.DrawLine(_rayEmmiter.transform.position, transform.position + _rayEmmiter.transform.forward * _interactDist, Color.red);
            Car otherCar = hit.transform.GetComponent<Car>();
            if (otherCar == null)
            {
                _isStuck = true;
                //_rayEmmiter can hit only one layer, if it is hit = it means car is not rotated wrong
            }
        }
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
    public void StartFleeing()
    {
        _flee = true;
        StartCoroutine("TakeTimeAndCourage"); // time for car to stop fleeing form player
    }
    public void DeathByTrail(int dmg)
    {
        //Debug.Log("DeathByTrail");
        _myHp.TakeDamage(dmg, _hitByTrail);
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
    IEnumerator TakeTimeAndCourage()
    {
        yield return new WaitForSeconds(_timeToForget);
        {
            if(seesCar == true)
            {
                seesCar = false;
            }
        }
    }
    IEnumerator BackUp()
    {
        rb.AddForce(-_thisAI.transform.forward * _speed * Time.deltaTime, ForceMode.Acceleration);
        yield return new WaitForSeconds(_backUp);
        {
            if (_isStuck == true)
            {
                _isStuck = false;
            }
        }
    }
    IEnumerator Forget()
    {
        yield return new WaitForSeconds(_timeToForget);
        {
            if (_flee == true)
            {
                _flee = false;
            }
        }
    }
}

