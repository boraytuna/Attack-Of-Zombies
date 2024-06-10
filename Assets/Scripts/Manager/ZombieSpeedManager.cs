using UnityEngine;

// A general script for keeping a central speed for all the zombies.
public static class ZombieSpeedManager
{
    public static float currentSpeed = 5f; // Initial speed
    public static float maxSpeed = 10f; // Example max speed
    public static float speedIncreaseRate = 0.5f; // Example speed increase rate

    // Method to increase the current speed over time, up to the max speed
    public static void IncreaseSpeed(float deltaTime)
    {
        currentSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * deltaTime, maxSpeed);
    }
}
