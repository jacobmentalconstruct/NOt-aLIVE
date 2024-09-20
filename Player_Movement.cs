using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour {

    private Vector2 mousePOS;
    private Vector3 target;
    [SerializeField] private float Rotation_GunOffset = 0.9f;
    private bool weAreFiring = false;
    private Vector3 nextRotationTarget;
    private GameObject bulletSpawner_OBJ;
    private Bullet_Spawner bulletSpawner_SCRIPT;
    private bool isIntro = true;
    [HideInInspector] public bool isPlayerDead;
    public UI_Controller _UI_Controller;

    private void Awake()
    {
        isPlayerDead = false;
        bulletSpawner_OBJ = GameObject.FindGameObjectWithTag("BulletSpawner");
        bulletSpawner_SCRIPT = bulletSpawner_OBJ.GetComponent<Bullet_Spawner>();

        if (_UI_Controller != null) {
             if(_UI_Controller.isIntro == true) { isIntro = true; }
             else { isIntro = false; 
             Debug.Log("isIntro is equal to: " + isIntro);
             } // Set isIntro to true or false pending what the UI_Controller is set to.
        } else { Debug.Log("Error: No UI_Controller is available."); }
           
       
    }
    private void Update()
    {
        if (isIntro == false && !isPlayerDead) {
            if (Input.GetButtonDown("Fire1"))
            {
                mousePOS = Input.mousePosition;
                RotatePlayer();
                weAreFiring = true;
            }
            if (weAreFiring == true && transform.rotation == Quaternion.LookRotation(nextRotationTarget)) {
                bulletSpawner_SCRIPT.Fire();
                weAreFiring = false;
            }
        }
    }

    private void RotatePlayer()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null) { 
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePOS.x, mousePOS.y, Camera.main.transform.position.y));
            Plane plane = new Plane(Vector3.up, bulletSpawner_OBJ.transform.position);
            float distance = 0.0f;

            if (plane.Raycast(ray, out distance))
            {
                target = ray.GetPoint(distance);
                nextRotationTarget = new Vector3(bulletSpawner_OBJ.transform.position.x, bulletSpawner_OBJ.transform.position.y, bulletSpawner_OBJ.transform.position.z) - target;
                Rigidbody playerRB = GetComponent<Rigidbody>();
                playerRB.MoveRotation(Quaternion.LookRotation(nextRotationTarget));
            }
        }
    }
}
