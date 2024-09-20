using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Weapon Game Objects Needed:")]
    [SerializeField] private GameObject weapon_GameObject;
    [SerializeField] private GameObject weaponSlot_GameObject;
    [SerializeField] private GameObject bulletSpawner_GameObject;
    private bool amIstillStanding = true;

    [Space(10)]

    [Header("Body Parts we need a reference to.")]
    [SerializeField] private GameObject body_GameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") {
            Destroy(gameObject);
            Debug.Log("amIstillStanding: " + amIstillStanding );
            other.transform.GetComponent<Enemy_Controller>().isEatingPlayer = true;
        }
    }

}
