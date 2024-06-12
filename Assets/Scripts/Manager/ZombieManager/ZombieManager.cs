using System.Collections.Generic;
using UnityEngine;

// Keeps track of zombies to move for now. In future when they killed it will be useful.
public class ZombieManager : MonoBehaviour
{
    private List<GamePlayZombieMovement> zombies = new List<GamePlayZombieMovement>(); // List to store zombie objects

    public void AddZombie(GamePlayZombieMovement zombie)
    {
        zombies.Add(zombie);
    }

    public void RemoveZombie(GamePlayZombieMovement zombie)
    {
        zombies.Remove(zombie);
    }

    public void MoveAllZombies()
    {
        foreach (var zombie in zombies)
        {
            zombie.MoveZombie();
        }
    }
}
