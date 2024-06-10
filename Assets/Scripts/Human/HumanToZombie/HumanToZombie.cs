using UnityEngine;

// This script manages humans turning to zombies when the collider is triggered.
public class HumanToZombie : MonoBehaviour
{
    public GameObject zombiePrefab; // Reference to the zombie prefab
    public LayerMask groundLayer; // Layer mask to define what layers are considered ground

    void Awake()
    {
        // Find an instance of the zombie in the scene by its tag
        GameObject existingZombie = GameObject.FindWithTag("Zombie");
        if (existingZombie != null)
        {
            // Get the prefab from the existing zombie instance
            zombiePrefab = existingZombie;
        }
        else
        {
            Debug.LogError("No zombie found with the tag 'Zombie'. Make sure there is a zombie in the scene with this tag.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            if (zombiePrefab != null)
            {
                // Find the ground position
                Vector3 spawnPosition = FindGroundPosition(transform.position);

                // Instantiate a new zombie at the correct ground position
                GameObject newZombie = Instantiate(zombiePrefab, spawnPosition, transform.rotation);

                // Set the speed of the new zombie to match the current global speed
                PlayerMovement playerMovement = newZombie.GetComponent<PlayerMovement>();

                // Destroy the human
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Zombie prefab is not assigned. Ensure a zombie is tagged with 'Zombie' in the scene.");
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
