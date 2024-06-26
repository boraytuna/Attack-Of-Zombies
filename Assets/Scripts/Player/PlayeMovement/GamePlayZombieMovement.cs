using UnityEngine;

public class GamePlayZombieMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private ZombieAnimatorController zombieAnimatorController;

    private Transform playerTransform; // Transform of the player
    private Vector3 initialDirection; // Initial direction vector towards the player on spawn
    private float initialDistance; // Initial distance from the player on spawn
    public float maxDistanceFromPlayer = 10f; // Maximum distance allowed between this zombie and the player

    public float baseSpeed = 7f; // Base speed of the zombie
    public float maxSpeedMultiplier = 2f; // Maximum speed multiplier for zombies far from the player
    public float stopDistance = 1f; // Distance within which the zombie will stop to avoid pushing the player

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        zombieAnimatorController = GetComponent<ZombieAnimatorController>();

        // Initialize playerTransform with the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Calculate initial direction and distance from player
        initialDirection = (playerTransform.position - transform.position).normalized;
        initialDistance = Vector3.Distance(transform.position, playerTransform.position);
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, playerTransform.position);

        if (currentDistance > stopDistance)
        {
            MoveZombie(currentDistance);
        }
        else
        {
            // Stop moving and play idle animation when within the stop distance
            _rigidbody.velocity = Vector3.zero;
            zombieAnimatorController.PlayIdle();
        }
    }

    private void MoveZombie(float currentDistance)
    {
        if (playerTransform != null)
        {
            // Update initial direction based on current player position
            initialDirection = (playerTransform.position - transform.position).normalized;

            // Adjust zombie speed based on distance from player
            float distanceRatio = Mathf.Clamp(currentDistance / maxDistanceFromPlayer, 0f, 1f);
            float speedMultiplier = Mathf.Lerp(1f, maxSpeedMultiplier, distanceRatio);

            // Set the zombie's speed
            float currentSpeed = baseSpeed * speedMultiplier;

            // Move the zombie towards the player at adjusted speed
            Vector3 targetVelocity = initialDirection * currentSpeed;
            _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

            // Rotate the zombie towards the movement direction
            if (initialDirection.magnitude > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, initialDirection, Time.deltaTime * 2f, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }

            // Play the appropriate animation based on movement
            if (initialDirection.magnitude > 0)
            {
                zombieAnimatorController.PlayRun();
            }
            else
            {
                zombieAnimatorController.PlayIdle();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with another zombie or the player
        if (collision.gameObject.CompareTag("Zombie") || collision.gameObject.CompareTag("Player"))
        {
            // Stop moving to prevent pushing
            _rigidbody.velocity = Vector3.zero;
            zombieAnimatorController.PlayIdle();
        }
    }
}
