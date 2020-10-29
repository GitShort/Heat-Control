using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{   
    [SerializeField] GameObject fire;
    [SerializeField] GameObject smoke;

    bool _isBurning = false;
    bool _changedColor = false;

    float startTimer = 0f;
    float colorChanger = 0f;
    [SerializeField] float colorChangeSpeed = 1f;

    [SerializeField] Renderer treeRend;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    public bool startBurning = false;

    Vector3 fireScaleChange;
    Vector3 treeScaleChange;
    [SerializeField] float fireScaleSpeed = 10f;
    [SerializeField] float treeScaleSpeed = 1f;

    [SerializeField] GameObject treeObject;

    bool _burntDown = false;

    [SerializeField] bool _isBush = false;

    float totalTime;
    [SerializeField] float duration = 3f;

    AudioSource aud;
    [SerializeField] AudioClip[] Sounds;

    bool stopFireAudio = false;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        smoke.SetActive(false);
        fire.SetActive(false);
        fireScaleChange = new Vector3(+0.01f, +0.01f, +0.01f) * Time.deltaTime * fireScaleSpeed;
        treeScaleChange = new Vector3(-0.01f, 0f, -0.01f) * Time.deltaTime * treeScaleSpeed;
    }

    void Update()
    {
        Timer();
        if (startBurning && totalTime >= duration && !_isBurning && !_burntDown)
        {
            Burn();
        }

        changeColor();

        if (_isBurning && !aud.isPlaying)
            aud.PlayOneShot(Sounds[0]);

    }

    void Burn()
    {
        fire.SetActive(true);
        _isBurning = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire" && !_burntDown)
        {
            Debug.Log("FIRE");
            fire.SetActive(true);
            _isBurning = true;
        }
        else if (other.tag == "Water" && _isBurning)
        {
            smoke.SetActive(true);
            aud.Stop();
            aud.PlayOneShot(Sounds[1], 30f);
            Invoke("StopBurning", 0.05f);
            _isBurning = false;
            if(!_burntDown)
            {
                GameManager.instance.ExtinguishedTreesCount++;
                _burntDown = true;
            }
        }
    }

    void changeColor()
    {
        if (fire.activeInHierarchy && !_changedColor)
        {
            startTimer = Time.time;
            _changedColor = true;
        }

        if (fire.activeInHierarchy)
        {
            colorChanger = (Time.time - startTimer) * colorChangeSpeed;
            treeRend.material.color = Color.Lerp(startColor, endColor, colorChanger);

  
            if (fire.transform.localScale.y < 1)
            {
                increaseSizeOverTime();
            }

            if (treeObject.transform.localScale.x > 0.1)
            {
                decreaseTreeSizeOverTime();
            }
            else
            {
                smoke.SetActive(true);
                Invoke("StopBurning", 0.05f);
                _isBurning = false;
                if (!_burntDown)
                {
                    GameManager.instance.burntTreesCount++;
                    _burntDown = true;
                    GameManager.instance.loseHealth();
                }
            }
        }
    }
    void StopBurning()
    {
        fire.SetActive(false);
    }

    void increaseSizeOverTime()
    {
        fire.transform.localScale += fireScaleChange;
    }

    void decreaseTreeSizeOverTime()
    {
        treeObject.transform.localScale += treeScaleChange;
    }

    void Timer()
    {
        if (totalTime <= duration)
        {
            totalTime += Time.deltaTime;
        }
    }


}
