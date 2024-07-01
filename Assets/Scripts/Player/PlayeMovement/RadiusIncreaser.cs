// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RadiusIncreaser : MonoBehaviour
// {
//     private ZombieCounter zombieCounter; // Reference to zombie counter script
//     private GamePlayZombieMovement zombieMovement; // Reference to zombie movement script

//     private void Start()
//     {
//         // Find the zombie counter script
//         zombieCounter = FindObjectOfType<ZombieCounter>();
//         // Find the zombie movement script
//         zombieMovement = FindObjectOfType<GamePlayZombieMovement>();
//     }

//     private void Update()
//     {
//         // Increase the follow radius based on the number of zombies
//         IncreaseFollowRadius();
//     }

//     // Method to increase the follow radius based on the number of zombies
//     public void IncreaseFollowRadius()
//     {
//         int zombieNumber = zombieCounter.GetZombieCount();
//         float newFollowRadius = 3f; // Default follow radius

//         // Adjust follow radius based on the number of zombies
//         if (zombieNumber >= 200)
//         {
//             newFollowRadius = 4.5f;
//         }
//         else if (zombieNumber >= 300)
//         {
//             newFollowRadius = 6f;
//         }

//         // Update the follow radius in the zombie movement script
//         if (zombieMovement != null)
//         {
//             zombieMovement.SetFollowRadius(newFollowRadius);
//         }

//         Debug.Log("Current Follow Radius: " + newFollowRadius);
//     }

// }
