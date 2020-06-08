using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slapper : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject _hitbox;
    [SerializeField] private ParticleSystem _slapstick;
    private float _timeToGo = 1f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Slap", true);
            StartCoroutine(FixTheSlap());
        }
    }
    public void JoystickSlap()
    {
        anim.SetBool("Slap", true);
        StartCoroutine(FixTheSlap());
    }
    public void Slap()
    {
        _slapstick.Play();
        _hitbox.SetActive(true);
    }
    public void Unslap()
    {
        _hitbox.SetActive(false);
    }
    IEnumerator FixTheSlap()
    {
        yield return new WaitForSeconds(_timeToGo);
        anim.SetBool("Slap", false);
        _hitbox.SetActive(false);
    }
}
