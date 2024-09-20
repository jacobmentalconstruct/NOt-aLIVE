using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CollisionManager : MonoBehaviour
{
    private Enemy_Controller controller;

    private void Awake()
    {
        ControllerINIT();
    }

    // Use OnTriggerEnter to detect the interaction
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Fence") || other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            // Call the correct method on the Enemy_Controller
            controller.OnTriggerEnter(other);  // Changed to use OnTriggerEnter
            Debug.Log("Enemy has collided with " + other.tag);
        }
    }

    private void ControllerINIT()
    {
        if (gameObject.GetComponentInParent<Enemy_Controller>())
        {
            controller = gameObject.GetComponentInParent<Enemy_Controller>();
        }
        else
        {
            Debug.LogError("EnemyController script is not found.");
        }
    }
}
