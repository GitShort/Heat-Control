using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : PlayerController
{
    [SerializeField] GameObject bucketWater;
    [SerializeField] ParticleSystem takeWaterParticles;
    [SerializeField] ParticleSystem pourWaterParticles;
    bool _isFilled = false;

    [SerializeField] AudioClip[] sounds;
    AudioSource aud;

    private void Start()
    {
        bucketWater.SetActive(false);
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if ((!_isFilled && _isNearWater) && Input.GetKeyDown(KeyCode.E))
        {
            aud.PlayOneShot(sounds[0]);
            playTakeWaterParticles();
            bucketWater.SetActive(true);
            _isFilled = true;
        }
        if (Input.GetKeyDown(KeyCode.F) && _isFilled)
        {
            aud.PlayOneShot(sounds[1]);
            playPourWaterParticles();
            bucketWater.SetActive(false);
            _isFilled = false;
        }
    }

    void playTakeWaterParticles()
    {
        takeWaterParticles.Play();
    }

    void playPourWaterParticles()
    {
        pourWaterParticles.Play();
    }
}
