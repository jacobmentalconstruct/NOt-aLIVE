using System.Collections.Generic;
using UnityEngine;

public class Fence_Controller : MonoBehaviour
{
    public List<Fence_HitPoints_Controller> fenceSegments = new List<Fence_HitPoints_Controller>();
    public List<Vector3> activeGaps = new List<Vector3>();  // Tracks destroyed segments

    [Header("Gap Settings")]
    public float gapCheckRadius = 1.0f;  // Radius to check around destroyed segments for zombies to pass through

    [Header("Zombie Management")]
    public int maxZombiesThroughGap = 5;  // Maximum number of zombies allowed through a gap
    private Dictionary<Vector3, int> zombiesPassedThroughGap = new Dictionary<Vector3, int>();  // Keeps track of zombies passing through each gap

    // Add a new segment to the fence
    public void AddSegment(Fence_HitPoints_Controller segment)
    {
        if (!fenceSegments.Contains(segment))
        {
            fenceSegments.Add(segment);
        }
    }

    // Clear all segments (used when regenerating the fence)
    public void ClearSegments()
    {
        fenceSegments.Clear();
        activeGaps.Clear();
        zombiesPassedThroughGap.Clear();
    }

    // Update gaps when a segment is destroyed or rebuilt
    public void UpdateGaps(Vector3 position)
    {
        if (!activeGaps.Contains(position))
        {
            activeGaps.Add(position);
            zombiesPassedThroughGap.Add(position, 0);  // Start tracking zombies passing through this gap
        }
    }

    // Check if there are any gaps at the given position
    public bool HasGapAtPosition(Vector3 position)
    {
        return activeGaps.Contains(position);
    }

    // Handle zombies passing through a gap
    public void ZombiePassedThroughGap(Vector3 gapPosition)
    {
        if (zombiesPassedThroughGap.ContainsKey(gapPosition))
        {
            zombiesPassedThroughGap[gapPosition]++;
        }
    }

    // Check if more zombies can pass through the gap
    public bool CanZombiePassThroughGap(Vector3 gapPosition)
    {
        if (zombiesPassedThroughGap.ContainsKey(gapPosition))
        {
            return zombiesPassedThroughGap[gapPosition] < maxZombiesThroughGap;
        }
        return false;
    }

    // Get the closest attackable segment for the zombie
    public Fence_HitPoints_Controller GetClosestAttackableSegment(Vector3 zombiePosition)
    {
        Fence_HitPoints_Controller closestSegment = null;
        float closestDistance = Mathf.Infinity;

        foreach (Fence_HitPoints_Controller segment in fenceSegments)
        {
            if (segment.CanZombieAttack())  // Only consider segments that can be attacked
            {
                float distance = Vector3.Distance(zombiePosition, segment.transform.position);
                if (distance < closestDistance)
                {
                    closestSegment = segment;
                    closestDistance = distance;
                }
            }
        }

        return closestSegment;
    }

    // Check if a zombie can attack a specific segment
    public bool CanZombieAttackSegment(Fence_HitPoints_Controller segment)
    {
        return segment != null && segment.CanZombieAttack();
    }

    // Get an available gap position that the zombie can pass through
    public Vector3? GetAvailableGap(Vector3 zombiePosition)
    {
        foreach (Vector3 gapPosition in activeGaps)
        {
            float distance = Vector3.Distance(zombiePosition, gapPosition);
            if (distance <= gapCheckRadius && CanZombiePassThroughGap(gapPosition))
            {
                return gapPosition;  // Return the first available gap the zombie can use
            }
        }

        return null;  // No available gap
    }

    // Additional logic related to fence segment management can be added here as needed
}
