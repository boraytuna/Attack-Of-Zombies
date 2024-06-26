using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health of the zombie
    private float currentHealth;    // Current health of the zombie

    void Start()
    {
        currentHealth = maxHealth;  // Initialize current health to max health
    }

    // Method to apply damage to the zombie
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;   // Reduce current health by the damage amount

        // Check if the zombie is dead
        if (currentHealth <= 0)
        {
            Die();  // Call the Die function if health drops to or below zero
        }
    }

    // Method to handle what happens when the zombie dies
    void Die()
    {
        Debug.Log("Zombie died!");
        // You can add more functionality here, such as playing death animations, dropping items, etc.
        Destroy(gameObject);  // Destroy the zombie GameObject
    }
}
