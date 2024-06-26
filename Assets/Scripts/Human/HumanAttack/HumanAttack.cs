using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttack : MonoBehaviour
{
    public float damageAmount = 20f;       // Amount of damage to apply per hit
    public float attackRate = 0.5f;        // Time between each attack in seconds
    private float nextAttackTime = 0f;     // Time when the next attack can occur

    void OnEnable()
    {
        HumanAttackDetector.OnShoot += ShootAtZombie;
    }

    void OnDisable()
    {
        HumanAttackDetector.OnShoot -= ShootAtZombie;
    }

    void Update()
    {
        // Update the cooldown timer
        if (Time.time >= nextAttackTime)
        {
            // If enough time has passed, allow shooting
            // This check ensures we respect the attack rate
            // For continuous shooting, this check is not needed in Update, 
            // but rather in the input or firing mechanism.
        }
    }

    void ShootAtZombie(Vector3 zombiePosition)
    {
        // Check if enough time has passed since the last attack
        if (Time.time >= nextAttackTime)
        {
            // Perform a raycast from the human position to the zombie position
            Vector3 direction = zombiePosition - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            // Check if the ray hits a game object on the zombie layer
            if (Physics.Raycast(ray, out hit, direction.magnitude, LayerMask.GetMask("Zombie")))
            {
                Debug.Log("Raycast hit object: " + hit.collider.gameObject.name);

                // Try to get the ZombieHealth component from the hit object
                ZombieHealth zombieHealth = hit.collider.gameObject.GetComponent<ZombieHealth>();
                if (zombieHealth != null)
                {
                    // Apply damage to the zombie
                    zombieHealth.TakeDamage(damageAmount);
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any zombie object");
            }

            // Set the next attack time based on the attack rate
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
}
