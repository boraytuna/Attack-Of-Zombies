using UnityEngine;

public class GamePlayZombieMovement : MonoBehaviour
{
    private ZombieManager zombieManager; // Reference to the ZombieManager
    private PlayerMovement playerMovement;
    [SerializeField] private AnimatorController _animatorController;
    private Rigidbody _rigidbody;

    private float speed;
    public float maxDistanceFromPlayer = 10f; // Maximum distance allowed between this zombie and the player zombie
    private float distanceFromPlayer; // Distance between this zombie and the player zombie
    public float closeSpeed = 2f; // Speed at which the zombie moves towards the player

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement not found in the scene.");
        }
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        zombieManager = FindObjectOfType<ZombieManager>();
        if (zombieManager != null)
        {
            zombieManager.AddZombie(this);
        }
        else
        {
            Debug.LogError("ZombieManager not found in the scene.");
        }
    }

    void Update()
    {
        // Update the speed based on whether the player is moving
        ZombieSpeedManager.IncreaseSpeed(Time.deltaTime);
        
        // Use the current speed for zombie movement
        speed = ZombieSpeedManager.currentSpeed;

        CalculateDistanceFromPlayer();
        CheckDistanceFromPlayer();

        // If the distance between player and this zombie object is more than the allowed distance
        if (distanceFromPlayer > maxDistanceFromPlayer)
        {
            CloseDistanceWithPlayer();
        }
        else
        {
            MoveZombie();
        }
    }

    public void MoveZombie()
    {
        Vector3 moveVector = playerMovement.GetMoveVector();

        // Move the zombie based on the player's movement
        Vector3 targetVelocity = moveVector * speed;
        targetVelocity = Vector3.ClampMagnitude(targetVelocity, speed);
        _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

        // Rotate the zombie towards the movement direction
        if (moveVector.magnitude > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveVector, playerMovement._rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        // Play the run animation if the zombie is moving
        if (moveVector.magnitude > 0)
        {
            _animatorController.PlayRun();
        }
        else
        {
            _animatorController.PlayIdle();
        }
    }

    // Calculate the distance between this zombie object and player zombie object
    private void CalculateDistanceFromPlayer()
    {
        if (playerMovement != null)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, playerMovement.transform.position);
        }
    }

    // Check if the distance is more than the allowed distance
    private void CheckDistanceFromPlayer()
    {
        if (distanceFromPlayer > maxDistanceFromPlayer)
        {
            Debug.Log("Zombie is too far from the player zombie!");
        }
    }

    // Close the distance if the distance is more than allowed
    private void CloseDistanceWithPlayer()
    {
        Vector3 direction = (playerMovement.transform.position - transform.position).normalized;
        Vector3 targetVelocity = direction * ZombieSpeedManager.currentSpeed * closeSpeed;
        targetVelocity = Vector3.ClampMagnitude(targetVelocity, ZombieSpeedManager.currentSpeed);
        _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);
        transform.rotation = Quaternion.LookRotation(direction);
        _animatorController.PlayRun();
    }
}
