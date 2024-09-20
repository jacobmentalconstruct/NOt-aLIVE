using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private Enemy_refLIB enemyLib;
    private GameObject currentTarget;
    private Fence_HitPoints_Controller fenceHealth;

    private void Start()
    {
        // Get reference to the Enemy_refLIB attached to this GameObject
        enemyLib = GetComponent<Enemy_refLIB>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            currentTarget = other.gameObject;
            fenceHealth = currentTarget.GetComponent<Fence_HitPoints_Controller>();

            if (fenceHealth != null && fenceHealth.CanZombieAttack())
            {
                fenceHealth.RegisterZombieAttack();
                InvokeRepeating("AttackFence", 0f, 1f); // Attack every second
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            if (fenceHealth != null)
            {
                fenceHealth.UnregisterZombieAttack();
            }

            CancelInvoke("AttackFence");
        }
    }

    private void AttackFence()
    {
        if (fenceHealth != null && !fenceHealth.IsDestroyed())  // Call the IsDestroyed() method
        {
            fenceHealth.TakeDamage(enemyLib.attackDamage);  // Get attackDamage from Enemy_refLIB
        }
    }
}
