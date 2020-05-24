using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCam : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private Transform _target;
    private float mouseX, mouseY;
    private int _limit = 60;
    private int _joyLimit = 30;
    [SerializeField] private bool _joystickControl = false;
    [SerializeField] private SimpleTouchController _camJoystick;
    private Vector3 _movement;
    private float _zeroVal = 0;
    private float _joyTurnVal = 0.5f;
    private float _joyRotFix = 2.5f;
    private bool _cameraMov = false;
    [SerializeField] private Transform _car;
    void Start()
    {
        if (_joystickControl == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void LateUpdate()
    {
        if (_joystickControl == false)
        {
            CamControl();
        }
        if (_joystickControl == true)
        {
            JoystickCamControl();
            if(_cameraMov == false)
            {
                //Cursor.lockState = CursorLockMode.Locked;
               // _target.transform.rotation = _car.transform.rotation;
            }
        }
    }
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * _rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * _rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -_limit, _limit);
        transform.LookAt(_target);
        _target.rotation = Quaternion.Euler(mouseY, mouseX, _zeroVal);
    }
    void JoystickCamControl()
    {
        mouseY = Mathf.Clamp(mouseY, -_joyLimit, _joyLimit);
        transform.LookAt(_target);
        _target.rotation = Quaternion.Euler(mouseY, mouseX, _zeroVal);
        Vector2 movement = _camJoystick.GetTouchPosition;
        _movement = new Vector3(movement.x, _zeroVal, movement.y);
        JoyTurnVal();
        JoyZFix();
    }
    void JoyZFix()
    {
        if (_movement.z != _zeroVal)
        {
            _cameraMov = true;
        }
        else
        {
            _cameraMov = false;
        }
    }
    void JoyTurnVal()
    {
        if (_movement.z > _zeroVal)
        {
            mouseY -= _rotationSpeed * _joyRotFix;// * Time.deltaTime;
        }
        if (_movement.z < _zeroVal)
        {
            mouseY += _rotationSpeed * _joyRotFix; //* Time.deltaTime;
        }
        if (_movement.x > _joyTurnVal)
        {
            mouseX += _rotationSpeed * _joyRotFix; //* Time.deltaTime;
        }
        if (_movement.x < -_joyTurnVal)
        {
            mouseX -= _rotationSpeed * _joyRotFix;// * Time.deltaTime;
        }
    }
}
