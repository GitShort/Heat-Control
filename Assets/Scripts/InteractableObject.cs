using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] float duration = 3f;
    float totalTime = 0;
    Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    private void Update()
    {
        Timer();
        //Debug.Log(totalTime);
        if (totalTime >= duration)
        {
            col.enabled = true;
        }
    }

    void Timer()
    {
        if (totalTime <= duration)
        {
            totalTime += Time.deltaTime;
            
        }
    }

}
