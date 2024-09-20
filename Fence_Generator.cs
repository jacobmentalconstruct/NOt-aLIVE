using UnityEngine;
using System.Collections.Generic;

public class Fence_Generator : MonoBehaviour
{
    public GameObject fenceSegmentPrefab; // Reference to the fence segment prefab
    public Transform startPoint;          // Start point of the fence
    public Transform endPoint;            // End point of the fence
    private GameObject fenceBarrierObject; // Parent object for the fence
    private Fence_Controller fenceController; // Reference to Fence_Controller
    public LayerMask groundLayer; // Layer mask to define the ground

    private void Start()
    {
        GenerateFence(); // Call the generation method on start
    }

    public void GenerateFence()
    {
        if (fenceSegmentPrefab == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("Missing reference in Fence_Generator.");
            return;
        }

        // Create a new parent object for the fence if it doesn't already exist
        if (fenceBarrierObject == null)
        {
            fenceBarrierObject = new GameObject("Fence_BarrierOBJ");
            fenceBarrierObject.transform.position = startPoint.position; // Set position to start point

            // Attach Fence_Controller script to the new parent object
            fenceController = fenceBarrierObject.AddComponent<Fence_Controller>();
        }

        // Check if Fence_Controller is successfully attached
        if (fenceController == null)
        {
            Debug.LogError("Failed to attach Fence_Controller component.");
            return;
        }

        // Clear existing segments if re-generating the fence
        fenceController.ClearSegments();

        // Calculate the total distance between the start and end points
        float totalDistance = Vector3.Distance(startPoint.position, endPoint.position);

        // Debug: Log total distance
        Debug.Log("Total distance between start and end points: " + totalDistance);

        // Find the BoxCollider from the prefab to get the total width
        float segmentLength = CalculateSegmentLengthUsingCollider(fenceSegmentPrefab);

        // Debug: Log segment length
        Debug.Log("Segment length: " + segmentLength);

        // Check for invalid total distance or segment length
        if (totalDistance <= 0)
        {
            Debug.LogError("Total distance is invalid: " + totalDistance);
            return;
        }

        if (segmentLength <= 0)
        {
            Debug.LogError("Segment length is invalid: " + segmentLength);
            return;
        }

        // Calculate the number of segments needed to cover the distance
        int segmentCount = Mathf.CeilToInt(totalDistance / segmentLength);

        // Debug: Log the number of segments
        Debug.Log("Calculated number of segments: " + segmentCount);

        // Ensure segment count is not negative
        if (segmentCount < 0)
        {
            Debug.LogError("Calculated negative segment count: " + segmentCount);
            segmentCount = 0;
        }

        // Generate fence segments
        Vector3 direction = (endPoint.position - startPoint.position).normalized;
        Vector3 currentPosition = startPoint.position;

        for (int i = 0; i < segmentCount; i++)
        {
            // Instantiate and position the segment
            GameObject segment = Instantiate(fenceSegmentPrefab, currentPosition, Quaternion.identity);
            segment.transform.SetParent(fenceBarrierObject.transform); // Set Fence_BarrierOBJ as the parent

            // Adjust segment to fit terrain
            AdjustSegmentToTerrain(segment);

            // Move to the next position
            currentPosition += direction * segmentLength;

            // Name the segment for clarity
            segment.name = "FenceSegment_" + i; 

            // Add to Fence_Controller
            Fence_HitPoints_Controller segmentController = segment.GetComponent<Fence_HitPoints_Controller>();
            if (segmentController != null)
            {
                fenceController.AddSegment(segmentController);
            }
            else
            {
                Debug.LogError("Fence segment does not have Fence_HitPoints_Controller attached.");
            }
        }

        Debug.Log("Fence generation complete with " + segmentCount + " segments.");
    }

    // Calculate the segment length using BoxCollider or MeshRenderer
    private float CalculateSegmentLengthUsingCollider(GameObject prefab)
    {
        BoxCollider collider = prefab.GetComponent<BoxCollider>();
        if (collider != null)
        {
            // Use the size of the BoxCollider's bounds for the segment length
            return collider.size.x * prefab.transform.localScale.x; // Account for scaling
        }

        // Fallback to using MeshRenderer if BoxCollider is not found
        return CalculateSegmentLengthUsingRenderer(prefab);
    }

    private float CalculateSegmentLengthUsingRenderer(GameObject prefab)
    {
        float totalWidth = 0f;
        MeshRenderer[] renderers = prefab.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            totalWidth += renderer.bounds.size.x; // Accumulate the width of all child renderers
        }

        return totalWidth;
    }

    private void AdjustSegmentToTerrain(GameObject segment)
    {
        // Raycast downwards from the segment's center to find the ground
        RaycastHit hit;
        Vector3 origin = segment.transform.position + Vector3.up * 10f; // Start the ray above the segment
        if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Set the segment's position to the hit point
            segment.transform.position = hit.point;

            // Calculate the rotation to align with the terrain's normal
            Vector3 normal = hit.normal;

            // Calculate the forward direction of the segment based on its current position and next position
            Vector3 forwardDirection = (endPoint.position - startPoint.position).normalized;

            // Calculate the right direction (cross product) to ensure proper alignment
            Vector3 right = Vector3.Cross(normal, forwardDirection).normalized;

            // Calculate the adjusted forward direction using the right direction and normal
            Vector3 adjustedForward = Vector3.Cross(right, normal).normalized;

            // Create a rotation that aligns the up vector with the hit normal and forward vector with the adjusted direction
            Quaternion targetRotation = Quaternion.LookRotation(adjustedForward, normal);

            // Apply the rotation, focusing on aligning the z-axis to fit naturally with the terrain
            segment.transform.rotation = Quaternion.Euler(segment.transform.rotation.eulerAngles.x, segment.transform.rotation.eulerAngles.y, targetRotation.eulerAngles.z);

            // Slightly lower the segment to ensure it's embedded in the ground
            segment.transform.position -= Vector3.up * 0.05f;
        }
    }
}
