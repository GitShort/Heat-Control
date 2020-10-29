using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerController : MonoBehaviour
{
    Collider col;
    Animator anim;
    AudioSource aud;

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        aud = GetComponent<AudioSource>();
        col.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Attack");
        }
        else
            anim.SetTrigger("AfterAttack");
    }

    void enableCollider()
    {
        col.enabled = true;
    }

    void disableCollider()
    {
        col.enabled = false;
    }

    void playSound()
    {
        aud.Play();
    }
}
