using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanSpeedManager : MonoBehaviour
{
    public static HumanSpeedManager Instance { get; private set; }

    private List<NavMeshAgent> humanAgents = new List<NavMeshAgent>(); // List for all the agents

    public float currentSpeed;  // Local variable for speed

    private ZombieSpeedManager zombieSpeedManager;  // Reference to ZombieSpeedManager for adjusting speed based on zombie speed

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

        // Find ZombieSpeedManager instance in the scene
        zombieSpeedManager = FindObjectOfType<ZombieSpeedManager>();
    }


    void Update()
    {
        // Calculate the target speed based on the current zombie speed
        currentSpeed = zombieSpeedManager.GetCurrentSpeed() * 0.85f;

        // Update the speed of all human agents
        foreach (NavMeshAgent agent in humanAgents)
        {
            agent.speed = currentSpeed;
        }
    }

}
