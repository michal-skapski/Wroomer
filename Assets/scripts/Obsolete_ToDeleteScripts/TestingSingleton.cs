using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSingleton : MonoBehaviour
{
    private static TestingSingleton _instance;

    public static TestingSingleton Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("No instance detected");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public void Test()
    {
        Debug.Log("Test");
    }
}
