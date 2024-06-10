using UnityEngine;

// This script counts the number of zombies in the map.
public class ZombieCounter : MonoBehaviour
{
    private int zombieCount = 1;

    public int GetZombieCount()
    {
        return zombieCount;
    }

    public void IncrementZombieCount()
    {
        zombieCount++;
        Debug.Log("Zombie Count: " + zombieCount);
    }

    public void DecrementZombieCount()
    {
        zombieCount--;
        Debug.Log("Zombie Count: " + zombieCount);
    }
}
