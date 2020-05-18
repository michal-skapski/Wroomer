using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private int damage = 2;
    private int _zeroVal = 0;
    [SerializeField] private GameObject _burst;
    void Update()
    {
        transform.Translate(_zeroVal, _zeroVal, speed * Time.deltaTime); 
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject projectile = Instantiate(_burst) as GameObject;
        projectile.transform.position = this.transform.position;
        //creating sparks on hit with other collider, when pooling is investigated check if it can be augmented here
        HP player = other.GetComponent<HP>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
