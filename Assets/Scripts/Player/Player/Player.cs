using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement; // Manages the player movement
    public ZombieManager zombieManager; // Manages the zombies
    public ZombieCounter zombieCounter; // Manages the zombie count
    // public ZombieHealth playerHealth; // Manages the player's health
    // public CollectibleManager collectibleManager; // Manages collectibles

    void Awake()
    {
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        if (zombieManager == null)
        {
            zombieManager = FindObjectOfType<ZombieManager>();
            if (zombieManager == null)
            {
                Debug.LogError("ZombieManager not found in the scene.");
            }
        }

        if (zombieCounter == null)
        {
            zombieCounter = FindObjectOfType<ZombieCounter>();
            if (zombieCounter == null)
            {
                Debug.LogError("ZombieCounter not found in the scene.");
            }
        }

        // if (playerHealth == null)
        // {
        //     playerHealth = GetComponent<ZombieHealth>();
        // }

        // if (collectibleManager == null)
        // {
        //     collectibleManager = FindObjectOfType<CollectibleManager>();
        // }
    }

    void Update()
    {
        // Integrate player movement
        playerMovement.Move();

        // Manage zombies
        //zombieManager.MoveAllZombies();

        // Update the UI (if any)
        UpdateUI();
        
    }

    void UpdateUI()
    {
        // Update the zombie counter UI
        if (zombieCounter != null)
        {
            // Update your UI elements here, e.g.,
            // zombieCounterText.text = "Zombies: " + zombieCounter.GetZombieCount();
        }
    }

    // public void TakeDamage(float damage)
    // {
    //     playerHealth.TakeDamage(damage);
    // }

    // public void CollectItem(CollectibleType type)
    // {
    //     if (collectibleManager != null)
    //     {
    //         collectibleManager.Collect(type);
    //     }
    // }
}
