using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    [HideInInspector] public LayerMask zombieLayerMask = 12;  // Layer mask to check nearby zombies

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        // Assign the zombie layer mask in the Inspector or set it programmatically here
    }

    public void MoveTowardsTarget(Vector3 targetPosition)
    {
        // Get nearby zombies within a radius
        Collider[] nearbyZombies = Physics.OverlapSphere(transform.position, 1f, zombieLayerMask);
        
        Vector3 separation = Vector3.zero;
        foreach (Collider zombie in nearbyZombies)
        {
            Vector3 directionAway = transform.position - zombie.transform.position;
            separation += directionAway.normalized / directionAway.magnitude;  // Repulsion logic
        }
        
        // Adjust movement direction with separation to avoid stacking
        Vector3 movementDirection = (targetPosition - transform.position).normalized + separation.normalized;
        agent.SetDestination(transform.position + movementDirection);
    }
}
