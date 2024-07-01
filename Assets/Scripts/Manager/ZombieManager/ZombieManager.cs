using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }

    public List<GameObject> zombies = new List<GameObject>(); // List to store zombie objects

    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private CircleContainer circleContainer; // Reference to the CircleContainer script

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
