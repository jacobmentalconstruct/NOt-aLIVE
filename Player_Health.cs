using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour {
    [SerializeField] private UI_Controller uiController_cs;
    [SerializeField] private Bullet_Spawner bulletSpawner_cs;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Enemy")
        {
            Debug.Log("Player is now dead");
            bulletSpawner_cs.isPlayerDead = true;
            gameObject.GetComponent<Player_Movement>().isPlayerDead = true;
            uiController_cs.EndGame();
        }
    }
}
