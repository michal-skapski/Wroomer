using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    [SerializeField] private GameObject _thisObject;
    private int _zeroVal = 0;
    //only visual for now, to see if it will work Updt/ it does
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AltCarMove>())
        {
            AltCarMove player = other.GetComponent<AltCarMove>();
            player.Nitro();
            Destroy(_thisObject, _zeroVal);
        }
    }
}
