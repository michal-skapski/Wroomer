using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCall : MonoBehaviour
{
    void Start()
    {
        TestingSingleton.Instance.Test();
    }
}
