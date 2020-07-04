using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltCarMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private float _movSpeed; //internal Speed
    [SerializeField] private float _rotSpeed = 1f;
    [SerializeField] private float _nitroTime = 7.5f;
    private Rigidbody rb;
    private Animator anim;
    private Animator bAnim;
    [SerializeField] private Transform _wheelz;
    [SerializeField] private Transform _secWheelz;
    [SerializeField] private GameObject _car;
    public static Transform otherObject;
    [SerializeField] private float _maxSpeed = 200f;
    private float _maxSpeedVal = 200f;
    private float _maxSpeedValNitro = 325f;
    [SerializeField] private ParticleSystem _wheelTrail;
    [SerializeField] private ParticleSystem _wheelTrail_2;
    private int _zeroVal = 0;
    private int _multiplier = 100;
    private int _speedMultiplier = 2;
    //groundCheck
    [SerializeField] private GameObject _rayEmmiter;
    public float _interactDist = 1;
    public bool isGrounded = true;
    //weapons available
    [SerializeField] private List<GameObject> _weapons;
    [SerializeField] private List<GameObject> _weaponsChecks;
    [SerializeField] private List<GameObject> _weaponButton; //for joystick
    private int _weapNum;
    public bool _hasMinigun = false;
    public bool _hasSlapper = false;
    public bool _hasRocketLauncher = false;
    public bool _hasSniperRifle = false;
    public bool _hasTrail = false;
    //menu/tut
    public static GameObject menuManager; //temporary for demo
    [SerializeField] private GameObject _tutorialPanel;
    public bool _destroyed = false;
    public bool _hidden = false;
    private bool _turned = false;
    private Menu _menu;
    [SerializeField] private bool _joystickControlled = true;
    //joystick mov
    private int _singleVal = 1;
    [SerializeField] private SimpleTouchController _leftJoystick;
    [SerializeField] private ParticleSystem _deadlyTrail;
    private Vector3 _movement;
    private float _joyTurnVal = 0.75f;
    private float _backUpTurnVal = 0.5f;
    void Awake()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            _joystickControlled = false;
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _joystickControlled = true;
        }
        rb = GetComponent<Rigidbody>();
        anim = _wheelz.GetComponent<Animator>();
        bAnim = _secWheelz.GetComponent<Animator>();
        _menu = menuManager.GetComponent<Menu>();
        _movSpeed = _moveSpeed;
    }
    void Update()
    {
        if (_destroyed == false)
        {
            if (_joystickControlled == false)
            {
                KeyboardMovement();
                SetCarStraight();
            }
            else if (_joystickControlled == true)
            {
                JoystickMovement();
            }
        }
        WeaponChange();
        InGameMenu();
        CheckForGround();
    }
    private void KeyboardMovement()
    {
        if (isGrounded == true)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, _maxSpeed); //limits speed by max speed
            otherObject.rotation = _wheelz.rotation; //sets compass to point to where car is facing / considering delete
            if (Input.GetKey(KeyCode.W))
            {
                GoingStraight(_movSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                GoingStraight(-_movSpeed);
            }
        }
        if (Input.GetKey(KeyCode.D)) //rotates wheels
        {
            RightTurn();
        }
        if (Input.GetKey(KeyCode.A))
        {
            LeftTurn();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeadlyTrail();
        }
    }
    private void JoystickMovement()
    {
        Vector2 movement = _leftJoystick.GetTouchPosition;
        _movement = new Vector3(movement.x, _zeroVal, movement.y);
        if (isGrounded == true)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, _maxSpeed);
            otherObject.rotation = _wheelz.rotation;
            if (_movement.z > _zeroVal)
            {
                GoingStraight(_movSpeed);
            }
            if (_movement.z < -_backUpTurnVal)
            {
                GoingStraight(-_movSpeed);
            }
        }
        if (_movement.x > _joyTurnVal && _movement.z > _zeroVal) //rotates wheels
        {
            RightTurn();
        }
        if (_movement.x < -_joyTurnVal && _movement.z > _zeroVal)
        {
            LeftTurn();
        }
        if (_movement.x > _joyTurnVal && _movement.z < _zeroVal) //rotates wheels
        {
            LeftTurn();
        }
        if (_movement.x < -_joyTurnVal && _movement.z < _zeroVal)
        {
            RightTurn();
        }
    }
    void GoingStraight(float _internalSpeed)
    {
        StraightTurn();
        var step = _rotSpeed * _multiplier * Time.deltaTime; //rotates towards wheels turn and moves forward using addforce
        _car.transform.rotation = Quaternion.RotateTowards(transform.rotation, otherObject.rotation, step);
        rb.AddForce(_car.transform.forward * _internalSpeed * Time.deltaTime, ForceMode.Acceleration);
    }
    public void SetCarStraight()
    { //sets car on the right course 
        if (isGrounded == false)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                this.transform.rotation = Quaternion.Euler(_zeroVal, transform.eulerAngles.y, _zeroVal);
            }
            else if (_joystickControlled == true)
            {
                if (_turned == false)
                {
                    this.transform.rotation = Quaternion.Euler(_zeroVal, transform.eulerAngles.y, _zeroVal);
                }
            }
        }
    }
    public void Die()
    {
        _destroyed = true;
    }
    private void WeaponChange() //weapons change using list and mouse scrolling
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ChangeWeaponForward();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeaponBackward();
        }
    }
    public void ChangeWeaponForward()
    {
        _weapNum++;
        if (_weapNum >= _weapons.Count)
        {
            _weapNum = _zeroVal;
        }
        WeaponPick();
    }
    public void ChangeWeaponBackward()
    {
        _weapNum--;
        if (_weapNum <= _zeroVal)
        {
            _weapNum = _zeroVal; //prevents from going outside of array - not a magic number
        }
        WeaponPick();
    }
    private void WeaponPick() //sets weapons other than selected inactive
    {
        foreach (var obj in _weapons)
            obj.SetActive(false);
        if (_weaponsChecks[_weapNum].activeInHierarchy == true)
        {
            _weapons[_weapNum].SetActive(true);
        }
        foreach (var obj in _weaponButton)
            obj.SetActive(false);
        if (_weaponsChecks[_weapNum].activeInHierarchy == true)
        {
            _weaponButton[_weapNum].SetActive(true);
        }
    }
    public void GainWeapon(int gunNum)
    {
        _weaponsChecks[gunNum].SetActive(true);
    }
    public void DeadlyTrail()
    {
        _deadlyTrail.Play();
    }
    private void CheckForGround() //checks if ground is beneath using short ray to see if "R" click and position restart can be used
    {
        Ray ray = new Ray(_rayEmmiter.transform.position, _rayEmmiter.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _interactDist))
        {
            Debug.DrawLine(_rayEmmiter.transform.position, transform.position + transform.forward * _interactDist, Color.green);
            isGrounded = true; //_rayEmmiter can hit only one layer, if it is hit = it means car is not rotated wrong
        }
        else
        {
            isGrounded = false;
        }
    }
    public void Nitro()
    {
        _wheelTrail.Play();
        _wheelTrail_2.Play();
        StartCoroutine(SpeedBoost());
    }
    IEnumerator SpeedBoost() //is called when nitro is used / changes speed values for short period of time
    {
        _movSpeed = _moveSpeed * _speedMultiplier; //multiplies base speed
        _maxSpeed = _maxSpeedValNitro; // boosted limit
        yield return new WaitForSeconds(_nitroTime);
        _movSpeed = _moveSpeed;
        _maxSpeed = _maxSpeedVal; //final limit
    }
    private void InGameMenu() //poprawione / 4 ify tylko teraz
    {
        if (Input.GetKeyUp(KeyCode.Tab)) //goes back to menu
        {
            GoToMenu();
        }
        if (Input.GetKeyUp(KeyCode.U)) //activates and deactivates tutorial panel
        {
            HideTutorial();
        }
    }
    public void GoToMenu()
    {
        _menu.GoToMenu(); //fixed
    }
    public void HideTutorial()
    {
        if (_hidden == false)
        {
            _tutorialPanel.SetActive(false);
            _hidden = true;
            Time.timeScale = _singleVal;
        }
        else if (_hidden == true)
        {
            _tutorialPanel.SetActive(true);
            _hidden = false; 
            Time.timeScale = _zeroVal;
        }
    }
    private void StraightTurn()
    {
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
        anim.SetBool("Straight", true);
        bAnim.SetBool("Right", false);
        bAnim.SetBool("Left", false);
        bAnim.SetBool("Straight", true);
    }
    private void LeftTurn()
    {
        anim.SetBool("Left", true);
        anim.SetBool("Right", false);
        anim.SetBool("Straight", false);
        bAnim.SetBool("Left", true);
        bAnim.SetBool("Right", false);
        bAnim.SetBool("Straight", false);
    }
    private void RightTurn()
    {
        anim.SetBool("Right", true);
        anim.SetBool("Left", false);
        anim.SetBool("Straight", false);
        bAnim.SetBool("Right", true);
        bAnim.SetBool("Left", false);
        bAnim.SetBool("Straight", false);
    }
}