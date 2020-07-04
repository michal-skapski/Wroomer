using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDrop : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _gun;
    [SerializeField] private int _speed = 0;
    [SerializeField] private bool _plane = true;
    public void Drop()
    {
        if (_plane == true)
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
        if (_plane == false)
        {
                GameObject projectile = Instantiate(_prefab) as GameObject;
                projectile.transform.position = _gun.transform.position;
                projectile.transform.rotation = _gun.transform.rotation;
        }
    }
}
