using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassCollision : MonoBehaviour
{
    [SerializeField] AudioSource aud;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hammer")
        {
            aud.Play();
            Destroy(this.gameObject);
            
        }
    }
}
