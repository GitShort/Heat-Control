using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGap : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject smoke;
    Collider col;
    AudioSource aud;

    private void Start()
    {
        fire.SetActive(true);
        smoke.SetActive(false);
        col = GetComponent<Collider>();
        aud = GetComponent<AudioSource>();
        col.enabled = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("YAS");
            fire.SetActive(false);
            smoke.SetActive(true);
            aud.Play();

            col.enabled = false;
        }
    }
}
