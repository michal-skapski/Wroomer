using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private GameObject _thisCar;
    private void Start()
    {
        _thisCar.GetComponent<AltCarMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _thisCar.GetComponent<AltCarMove>().isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _thisCar.GetComponent<AltCarMove>().isGrounded = false;
    }
}
