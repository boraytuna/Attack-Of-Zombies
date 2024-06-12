using UnityEngine;
using System;

// This script is attached to the main human object 
// and it checks if a zombie objects are in range.
public class ZombieDetection : MonoBehaviour
{
    public float detectionRange = 10f; // Range for detecting zombies
    private Transform playerTransform; // Reference to the player (assumed to be the zombie)

    public static event Action<Vector3> OnZombieDetected; // Event triggered when zombie is detected

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find player GameObject
    }

    void Update()
    {
        // Check if the player (zombie) is within detection range
        if (Vector3.Distance(transform.position, playerTransform.position) < detectionRange)
        {
            Debug.Log("Zombie detected"); // Log detection
            OnZombieDetected?.Invoke(playerTransform.position); // Trigger zombie detection event
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Draw detection range sphere
    }
}
