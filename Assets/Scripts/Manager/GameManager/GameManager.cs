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
    // public List<GamePlayZombieMovement> zombies = new List<GamePlayZombieMovement>();

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
        // Initialize systems if necessary
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

        // // Initialize all zombies in the scene
        // foreach (var zombie in zombies)
        // {
        //     zombie.Initialize(playerMovement);
        // }

        // Register playerMovement to zombieManager
        zombieManager.Initialize(playerMovement);
    }
}
