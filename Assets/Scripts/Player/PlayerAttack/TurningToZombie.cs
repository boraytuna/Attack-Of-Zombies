using UnityEngine;

// This script work with zombieCounter and HumanToZombie script to turn the humans into zombies.
public class TurningToZombie : MonoBehaviour
{
    private ZombieCounter zombieCounter;

    private void Start()
    {
        zombieCounter = FindObjectOfType<ZombieCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            // Check if this object is tagged as a zombie
            if (gameObject.CompareTag("Zombie"))
            {
                // Turn the human into a zombie
                other.gameObject.tag = "Zombie";
                // Increment the zombie count
                zombieCounter.IncrementZombieCount();
            }
        }
    }

}
