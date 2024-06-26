using UnityEngine;

// This script manages a central speed for each zombie.
public static class ZombieSpeedManager
{
    public static float currentSpeed = 5f; // Initial speed
    public static float maxSpeed = 15f; // Example max speed
    public static float speedIncreaseRate = 1f; // Example speed increase rate
    public static float middleSpeed = 10f; // Middle speed

    private static bool isMoving = false; // Flag to track if the player is moving

    public static void SetMoving(bool moving)
    {
        isMoving = moving;
    }

    public static void IncreaseSpeed(float deltaTime)
    {
        if (isMoving)
        {
            currentSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * deltaTime, maxSpeed);
        }
        else
        {
            currentSpeed = Mathf.Max(currentSpeed - speedIncreaseRate * deltaTime, middleSpeed);
        }
        // Ensure the current speed never goes below middle speed or above max speed
        currentSpeed = Mathf.Clamp(currentSpeed, middleSpeed, maxSpeed);
    }
}