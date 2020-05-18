using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGet : MonoBehaviour
{
    private int _number = 10;
    private string _name = "Jan";
   public int Number
   {
        get
        { //gets value
            Debug.Log("Get");
            DoubleNumbers(_number);
            return _number * 3;
        }
        set
        { //and sets a new one for further getting
            Debug.Log("set");
            _number = value;
            Debug.Log("new number is:" + _number);
            if(_number > 100)
            {
                Debug.Log("Whoa...");
            }
            else
            {
                Debug.Log("Nah");
            }
        }
   }
    private void Start()
    {
        Debug.Log("number is:" + Number);
        Number = 20;
    }
    void DoubleNumbers(int _num)
    {
        _number = _num * 2;
    }
}
