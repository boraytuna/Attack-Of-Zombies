using UnityEngine;

// This script turn human objects to zombie objects. 
// Increments the number of zombies for the zombie counter script.
public class HumanToZombie : MonoBehaviour
{
    public LayerMask groundLayer; // Layer mask to define what layers are considered ground
    private ZombieCounter zombieCounter; // Reference to the Zombie Counter script
    public GameObject zombiePrefab; // Reference to the zombie prefab, assigned in the inspector

    private void Start()
    {
        zombieCounter = FindObjectOfType<ZombieCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie") || other.gameObject.CompareTag("Player"))
        {
            if (zombiePrefab != null)
            {
                // Find the ground position
                Vector3 spawnPosition = FindGroundPosition(transform.position);

                // Instantiate a new zombie at the correct ground position
                GameObject newZombie = Instantiate(zombiePrefab, spawnPosition, transform.rotation);

                // Increment the zombie count
                zombieCounter.IncrementZombieCount();

                // Destroy the human
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Zombie prefab is not assigned. Assign the zombie prefab in the inspector.");
            }
        }
    }

    Vector3 FindGroundPosition(Vector3 originalPosition)
    {
        Vector3 rayStart = originalPosition + Vector3.up * 10f; // Start the raycast from above the object
        Ray ray = new Ray(rayStart, Vector3.down); // Cast the ray downwards

        if (Physics.Raycast(ray, out RaycastHit hit, 20f, groundLayer))
        {
            return hit.point; // Return the point where the ray hit the ground
        }

        return originalPosition; // If no ground was found, return the original position
    }
}
