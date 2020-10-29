using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] Animator bodyAnim;

    public float movespeed = 5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] GameObject[] interactableObjects;
    [SerializeField] GameObject[] droppedObjects;
    GameObject currentObject;
    int equipped = 0; // 0 - no tool, 1 - bucket, 2 - fire extinguisher, 3 - hammer
    bool _isEquipped = false;

    public static bool _isNearWater;

    Vector3 velocity;
    [SerializeField] float gravity = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        _isNearWater = false;
        foreach (var tool in interactableObjects)
        {
            tool.SetActive(false);
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * movespeed * Time.deltaTime);
            bodyAnim.SetBool("isWalking", true);
        }
        else
            bodyAnim.SetBool("isWalking", false);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (equipped == 1 && !_isEquipped)
        {
            currentObject = interactableObjects[0];
            objectPickup(currentObject);
        }
        if (equipped == 2 && !_isEquipped)
        {
            currentObject = interactableObjects[1];
            objectPickup(currentObject);
        }
        if (equipped == 3 && !_isEquipped)
        {
            currentObject = interactableObjects[2];
            objectPickup(currentObject);
        }

        if (Input.GetKeyDown(KeyCode.G) && _isEquipped)
        {
            objectDrop(currentObject);
        }
    }

    void objectPickup(GameObject obj)
    {
        obj.SetActive(true);
        _isEquipped = true;
    }

    void objectDrop(GameObject obj)
    {
        obj.SetActive(false);
        _isEquipped = false;
        Instantiate(droppedObjects[equipped - 1], transform.position, transform.rotation);
        equipped = 0;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bucket" && !_isEquipped)
        {
            Destroy(other.gameObject);
            equipped = 1;
        }

        if(other.gameObject.tag == "FireExt" && !_isEquipped)
        {
            Destroy(other.gameObject);
            equipped = 2;
        }

        if (other.gameObject.tag == "Hammer" && !_isEquipped)
        {
            Destroy(other.gameObject);
            equipped = 3;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            _isNearWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            _isNearWater = false;
        }
    }
}
