using UnityEngine;

// This script detects the zombie objects from a human object to 
// create a group that will run away from the zombie together.
public class ZombieDetection : MonoBehaviour
{
    public float detectionRange = 5f; // Range to detect zombies
    public LayerMask zombieLayer; // Layer where zombies are placed

    public bool zombieInRange = false; // Flag to track if a zombie is in range

    void Update()
    {
        // Check if any zombie is in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, zombieLayer);
        if (hitColliders.Length > 0)
        {
            // If a zombie is detected, set the flag and debug
            if (!zombieInRange)
            {
                zombieInRange = true;
                Debug.Log("Zombie in range!");
            }
        }
        else
        {
            // If no zombie is detected, reset the flag
            zombieInRange = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection range in red
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
