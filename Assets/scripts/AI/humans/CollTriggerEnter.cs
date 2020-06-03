using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollTriggerEnter : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private bool _myTrigger = false;
    void OnTriggerEnter(Collider other)
    {
        HP player = other.GetComponent<HP>();
        if (player != null)
        {
            player.TakeDamage(_damage, _myTrigger);
        }
    }
}
