using UnityEngine;
using UnityEngine.UI;

public class Fence_HitPoints_Controller : MonoBehaviour
{
    [Header("Fence Stats")]
    public int maxHP = 50;
    [HideInInspector] public int currentHP;
    public int repairAmount = 10;
    public int rebuildCost = 50;
    public int armorClass = 0;
    public int damageToZombies = 0;
    public int zombieAttackLimit = 3;
    [HideInInspector] public int currentAttackers = 0;
    public Text myHP_Display;

    private Animator animator;
    private Fence_Controller fenceParent;
    private bool _isDestroyed = false;  // Tracks whether the fence is destroyed

    private void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        fenceParent = GetComponentInParent<Fence_Controller>();
    }

    // Method to take damage from zombies
    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.Max(damage - armorClass, 0);
        currentHP -= finalDamage;

        if (myHP_Display != null)
            myHP_Display.text = currentHP.ToString();

        if (animator != null)
            animator.SetTrigger("Hit");

        if (currentHP <= 0)
        {
            DestroySegment();
        }
    }

    private void DestroySegment()
    {
        currentHP = 0;
        _isDestroyed = true;  // Set to true when destroyed
        animator.SetTrigger("Die");

        GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;

        if (fenceParent != null)
        {
            fenceParent.UpdateGaps(transform.position);
        }
    }

    // Method for repairing the fence segment
    public void Repair()
    {
        currentHP = Mathf.Min(currentHP + repairAmount, maxHP);
        _isDestroyed = false;  // Set to false when repaired
        myHP_Display.text = currentHP.ToString();

        if (animator != null)
            animator.SetTrigger("Repair");

        if (currentHP > 0)
        {
            GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
        }
    }

    public bool IsDestroyed()  // This method is used to check if the segment is destroyed
    {
        return _isDestroyed;
    }

    public void RegisterZombieAttack()
    {
        if (currentAttackers < zombieAttackLimit)
        {
            currentAttackers++;
        }
    }

    public void UnregisterZombieAttack()
    {
        currentAttackers = Mathf.Max(0, currentAttackers - 1);
    }

    public bool CanZombieAttack()
    {
        return currentAttackers < zombieAttackLimit;
    }
}
