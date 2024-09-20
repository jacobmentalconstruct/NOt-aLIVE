using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    private Enemy_refLIB enemyLib;

    private void Start()
    {
        // Get reference to the Enemy_refLIB attached to this GameObject
        enemyLib = GetComponent<Enemy_refLIB>();
    }

    public void TakeDamage(int damage)
    {
        // Apply AC to reduce incoming damage
        int effectiveDamage = Mathf.Max(0, damage - enemyLib.armorClass);
        enemyLib.currentHP -= effectiveDamage;

        if (enemyLib.currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject); // Handle enemy death here
    }
}
