using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDrop : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _gun;
    [SerializeField] private int _speed = 0;
    public void Drop()
    {
        if (_gun != null)
        {
            GameObject projectile = Instantiate(_prefab) as GameObject;
            projectile.transform.position = transform.position + _gun.transform.forward;
            projectile.transform.rotation = _gun.transform.rotation;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = _gun.transform.forward * _speed;
        }
    }
}
