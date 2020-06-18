using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private float _zeroVal = 0f;
    void OnTriggerEnter(Collider other)
    {
        Destroy(other, _zeroVal);
    }
}
