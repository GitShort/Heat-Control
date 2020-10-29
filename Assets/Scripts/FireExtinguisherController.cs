using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class FireExtinguisherController : MonoBehaviour
{
    [SerializeField] ParticleSystem pourSmokeParticles;
    [SerializeField] float capacity = 50f;
    bool _isEmitting;

    AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            pourSmokeParticles.Emit(1);
            if (!aud.isPlaying)
            {
                aud.Play();
                _isEmitting = true;
            }
        }
        else
            _isEmitting = false;
        if (!_isEmitting)
            aud.Stop();
        

    }
}

