using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour {

    [SerializeField] private float speed = 50f;
    private Rigidbody rb;
    public int dmg;
    private Vector3 direction;
    public AudioClip[] shotFired_CLIPS;

    private void Start()
    {
        direction = transform.forward;
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * -speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        string colTag = other.tag;
        string colName = other.name;
        string myName = gameObject.name;

       if (colName != "WorldBoundary" && colTag != "Fence" && colTag != "Player" ) { Destroy(gameObject); }
    }
}
