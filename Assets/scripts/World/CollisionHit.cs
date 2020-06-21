using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHit : MonoBehaviour
{
    [SerializeField] private int _damage = 200;
    [SerializeField] private bool _myTrigger = false;
    void OnTriggerEnter(Collider other)
    {
        /*
        HP player = other.GetComponent<HP>();
        if (player != null)
        {
            player.TakeDamage(_damage, _myTrail);
        }
        */
        HP player = other.GetComponent<HP>();
        if (player != null)
        {
            player.TakeDamage(_damage, _myTrigger);
        }
    }
}
