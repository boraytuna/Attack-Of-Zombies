using UnityEngine;
using System;

// This script is attached to the main police object 
// and it checks if zombie objects are in range.
public class HumanAttackDetector : MonoBehaviour
{
    public float detectionRange = 10f; // Range for detecting zombies
    public LayerMask zombieLayer; // Layer mask for detecting zombies

    public static event Action<Vector3> OnShoot; // Event triggered when shooting

    void Update()
    {
        // Find all colliders within the detection range that are on the zombie layer
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, zombieLayer);
        foreach (Collider collider in colliders)
        {
            Transform zombieTransform = collider.transform;
            if (zombieTransform != null)
            {
                Debug.Log("Zombie detected, preparing to shoot"); // Log detection
                OnShoot?.Invoke(zombieTransform.position); // Trigger shooting event
                break; // Break after detecting the first zombie in range
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Draw detection range sphere
    }
}
