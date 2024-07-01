using UnityEngine;

// This script is attached to human object, responsible for 
// turning humans to zombie on trigger.
public class HumanToZombie : MonoBehaviour
{
    public LayerMask groundLayer; // Layer mask to define what layers are considered ground
    public GameObject zombiePrefab; // Reference to the zombie prefab, assigned in the inspector
    private ZombieManager zombieManager; // Reference to zombie manager script
    private ZombieCounter zombieCounter; // Reference to the Zombie Counter script

    private void Start()
    {
        zombieCounter = FindObjectOfType<ZombieCounter>();
        if (zombieCounter == null)
        {
            Debug.LogError("ZombieCounter not found in the scene.");
        }

        zombieManager = FindObjectOfType<ZombieManager>();
        if (zombieManager == null)
        {
            Debug.LogError("ZombieManager not found in the scene.");
        }
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
                GameObject newZombieObject = Instantiate(zombiePrefab, spawnPosition, transform.rotation);
                
                // Add the zombie to the ZombieManager
                zombieManager.AddZombie(newZombieObject);

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
