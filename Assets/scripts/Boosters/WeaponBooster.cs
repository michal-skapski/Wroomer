using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBooster : MonoBehaviour
{
    [SerializeField] private GameObject _thisObject;
    [SerializeField] private float _timeToDestroy = 0f;
    [SerializeField] private bool _mini = false;
    [SerializeField] private bool _rocket = false;
    [SerializeField] private bool _sniper = false;
    [SerializeField] private bool _slapper = false;
    [SerializeField] private bool _trail = false;
    private int _miniVal = 0;
    private int _rocketVal = 1;
    private int _sniperVal = 2;
    private int _slapperVal = 3;
    private int _trailVal = 4;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AltCarMove>()) //boole -> enum , switch
        {
            AltCarMove player = other.GetComponent<AltCarMove>();
            if (_mini == true)
            {
                player._hasMinigun = true;
                player.GainWeapon(_miniVal);
            }
            if (_rocket == true)
            {
                player._hasRocketLauncher = true;
                player.GainWeapon(_rocketVal);
            }
            if (_sniper == true)
            {
                player._hasSniperRifle = true;
                player.GainWeapon(_sniperVal);
            }
            if (_slapper == true)
            {
                player._hasSlapper = true;
                player.GainWeapon(_slapperVal);
            }
            if (_trail == true)
            {
                player._hasTrail = true;
                player.GainWeapon(_trailVal);
            }
            Destroy(_thisObject, _timeToDestroy);
        }
    }
}
