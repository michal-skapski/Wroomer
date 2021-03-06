﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 2;
    [SerializeField] private bool _playerMade = true;
    void OnTriggerEnter(Collider other)
    {
        HP player = other.GetComponent<HP>();
        if (player != null)
        {
            player.TakeDamage(damage, _playerMade);
        }
    }
}
