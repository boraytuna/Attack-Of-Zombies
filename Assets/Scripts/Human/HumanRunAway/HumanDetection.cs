using System;
using System.Collections.Generic;
using UnityEngine;

// This script detects the human objects around a human object to 
// create a group that will run away from the zombie together.
public class HumanDetection : MonoBehaviour
{
    public float detectionRange = 5f; // Range to detect other humans
    public string humanTag = "Human"; // Tag for detecting humans
    public float spawnDistance = 10f; // Distance to spawn humans away from zombie

    public event Action<Transform> OnHumanDetected; // Event for when a human is detected

    private bool hasDetected = false; // Flag to track if a detection has occurred

    void Update()
    {
        // If a detection has already occurred, return early
        if (hasDetected)
        {
            return;
        }

        // Check for nearby humans
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider col in hitColliders)
        {
            if (col.transform != transform && col.CompareTag(humanTag))
            {
                Debug.Log("Human detected");

                // Calculate a spawn position away from the zombie
                Vector3 spawnPosition = transform.position + (col.transform.position - transform.position).normalized * spawnDistance;

                OnHumanDetected?.Invoke(col.transform); // Invoke the event with the detected human's transform
                hasDetected = true; // Set the flag to true once a detection occurs
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection range in blue
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
