using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollHit : MonoBehaviour
{
    [SerializeField] private int _damage = 200;
    void OnParticleCollision(GameObject other)
    {
        AIEnemy player = other.GetComponent<AIEnemy>();
        if (player != null)
        {
            player.DeathByTrail(_damage);
        }
    }
}

