using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player player;
    public ZombieCounter zombieCounter;
    public List<HumanManager> humanManagers = new List<HumanManager>();
    public HumanSpawner humanSpawner;
    public PlayerMovement playerMovement;
    public ZombieManager zombieManager;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager
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

    private void Start()
    {
        // Initialize systems
        InitializeSystems();
    }

    private void InitializeSystems()
    {
        // Initialize or start systems if needed
        foreach (var humanManager in humanManagers)
        {
            humanManager.Initialize();
        }

        humanSpawner.SpawnHumans();

        // Initialize the player's movement
        playerMovement.Initialize();

        // Register playerMovement to zombieManager
        zombieManager.Initialize(playerMovement);
    }
}
