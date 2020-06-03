using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollHit : MonoBehaviour
{
    [SerializeField] private int _damage = 200;
    [SerializeField] private bool _myTrail = true;
    void OnParticleCollision(GameObject other)
    {
        /*
        HP player = other.GetComponent<HP>();
        if (player != null)
        {
            player.TakeDamage(_damage, _myTrail);
        }
        */
        AIEnemy player = other.GetComponent<AIEnemy>();
        if (player != null)
        {
            player.DeathByTrail(_damage);
        }
    }
}

