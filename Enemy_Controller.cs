using System.Collections;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Enemy_refLIB enemyLib;  // Reference to the centralized script
    private Enemy_Health enemyHealth;
    private Enemy_Movement enemyMovement;
    private Enemy_Attack enemyAttack;
    private Enemy_Audio enemyAudio;

    [HideInInspector] public bool isEatingPlayer;
    [HideInInspector] public bool attackFence_Running = false;

    private void Awake()
    {
        enemyLib = GetComponent<Enemy_refLIB>();  // Get reference to the central script
        enemyHealth = GetComponent<Enemy_Health>();
        enemyMovement = GetComponent<Enemy_Movement>();
        enemyAttack = GetComponent<Enemy_Attack>();
        enemyAudio = GetComponent<Enemy_Audio>();

        isEatingPlayer = false;
        BuffStats();  // Buff based on current wave
    }

    // Buff stats based on the current wave
    private void BuffStats()
    {
        int currentWave = GameObject.Find("EnemySpawner").GetComponent<Enemy_Spawner>().currentWave;

        // Apply buffs based on current wave
        enemyLib.maxHP += (currentWave - 1);
        enemyLib.armorClass += (currentWave - 1);
        enemyLib.pointValue += (currentWave - 1);
        enemyLib.movementSpeed += (currentWave - 1);

        enemyLib.currentHP = enemyLib.maxHP;  // Ensure health is synced
    }

    // Handle collisions (e.g., with fences, bullets, or the player)
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            StartCoroutine(AttackFence_Routine(other.gameObject));
        }
        else if (other.CompareTag("Bullet"))
        {
            GetComponent<Enemy_Health>().TakeDamage(other.GetComponent<Bullet_Controller>().dmg);
        }
    }

    // Coroutine to handle attacking the fence
    IEnumerator AttackFence_Routine(GameObject target)
    {
        attackFence_Running = true;
        while (target != null)
        {
            Fence_HitPoints_Controller fenceHP = target.GetComponent<Fence_HitPoints_Controller>();
            if (fenceHP != null)
            {
                fenceHP.TakeDamage(enemyLib.attackDamage);
                yield return new WaitForSeconds(1);
            }
            else
            {
                break;  // Exit loop if the target is no longer valid
            }
        }

        // After the fence is destroyed, attack the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            enemyMovement.MoveTowardsTarget(player.transform.position);
        }
        attackFence_Running = false;
    }

    // Handle enemy death and game-related effects (e.g., score, wave tracking)
    public void OnDeath()
    {
        UI_Controller uiController = GameObject.FindGameObjectWithTag("UI_Canvass").GetComponent<UI_Controller>();
        if (uiController != null)
        {
            uiController.UpdateScore(enemyLib.pointValue);  // Update the score
        }

        // Reduce enemy count in the spawner
        Enemy_Spawner spawner = GameObject.Find("EnemySpawner").GetComponent<Enemy_Spawner>();
        if (spawner != null)
        {
            spawner.totalEnemyCount--;
            if (enemyLib.isBossAlive)
            {
                enemyLib.isBossAlive = false;
                spawner.isBossAlive = false;
            }
        }

        Destroy(gameObject);  // Destroy the enemy after death
    }

    // Triggered when the enemy attacks the player
    public void EatTheHuman()
    {
        Debug.Log("The enemy is eating the human.");
        isEatingPlayer = true;
        // Additional logic to handle attacking/eating the player
    }
}
