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

    public void AddZombie(GameObject zombie)
    {
        zombies.Add(zombie);
    }

    public void RemoveZombie(GameObject zombie)
    {
        zombies.Remove(zombie);
    }
}
