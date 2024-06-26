// using System.Collections.Generic;
// using UnityEngine;

// public class ZombieManager : MonoBehaviour
// {
//     public List<GamePlayZombieMovement> zombies = new List<GamePlayZombieMovement>(); // List to store zombie objects

//     private PlayerMovement playerMovement; // Reference to the PlayerMovement script

//     public void Initialize(PlayerMovement playerMovement)
//     {
//         this.playerMovement = playerMovement;
//     }

//     public void AddZombie(GamePlayZombieMovement zombie)
//     {
//         zombies.Add(zombie);
//     }

//     public void RemoveZombie(GamePlayZombieMovement zombie)
//     {
//         zombies.Remove(zombie);
//     }

//     // public void MoveAllZombies()
//     // {
//     //     foreach (var zombie in zombies)
//     //     {
//     //         zombie.MoveZombie();
//     //     }
//     // }
// }
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public List<GameObject> zombies = new List<GameObject>(); // List to store zombie objects

    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private CircleContainer circleContainer; // Reference to the CircleContainer script

        public void Initialize(PlayerMovement playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    void Start()
    {
        circleContainer = FindObjectOfType<CircleContainer>();
        if (circleContainer == null)
        {
            Debug.LogError("CircleContainer not found in the scene.");
        }
    }

    public void AddZombie(GameObject zombie)
    {
        zombies.Add(zombie);
        circleContainer.AddZombie(zombie);
    }

    public void RemoveZombie(GameObject zombie)
    {
        zombies.Remove(zombie);
        circleContainer.RemoveZombie(zombie);
    }
}
