using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float _time = 1.35f;
    [SerializeField] private GameObject _object;

    void Update()
    {
        Destroy(_object, _time);
    }
}
