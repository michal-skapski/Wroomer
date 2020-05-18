using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _thisObject;
    [SerializeField] private float _timeToDestroy = 0f;
    //only visual for now, to see if it will work Updt/ it does
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AltCarMove>())
        {
            HP player = other.GetComponent<HP>();
            player.Shield();
            Destroy(_thisObject, _timeToDestroy);
        }
    }
}
