using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenSend : MonoBehaviour
{
    [SerializeField] private GameObject _lilHuman;
    private Animator anim;
    [SerializeField] private bool _isEnemy = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        _lilHuman.GetComponent<LilHum>();
        if (_isEnemy == true)
        {
            anim.SetBool("Aim", true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AltCarMove>())
        {
            DieInnocentOne();
            //Debug.Log("HehsCar");
        }
        else if (other.GetComponent<Destroy>())
        {
            DieInnocentOne();
            //Debug.Log("HehsPro");
        }
        else if (other.GetComponent<HP>())
        {
            DieInnocentOne();
            //Debug.Log("HehsCarzie");
        }
        else if (other.GetComponent<Explosion>())
        {
            DieInnocentOne();
            //Debug.Log("Oh, cmon!");
        }
    }
    void DieInnocentOne()
    {
        anim.SetBool("RunOver", true);
        anim.SetBool("Aim", false);
        _lilHuman.GetComponent<LilHum>().Die();
    }
}
