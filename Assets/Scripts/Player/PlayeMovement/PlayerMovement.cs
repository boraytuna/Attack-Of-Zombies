using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// This script is responsible for moving the zombie player.
public class PlayerMovement : MonoBehaviour, ICharacter
{
    [SerializeField] private FloatingJoystick _joystick; // Reference to the floating joystick
    [SerializeField] private AnimatorController _animatorController; // Reference to the animator controller
    [SerializeField] public float _rotateSpeed; // Rotation speed of the player

    private float speed; // Reference to the speed
    private Rigidbody _rigidbody; // Reference to the Rigidbody component
    private Vector3 _moveVector; // Vector for storing movement input
    [SerializeField] public bool IsMoving; // Boolean to keep track of movement for speed

    private ZombieManager zombieManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        zombieManager = FindObjectOfType<ZombieManager>();
        if (zombieManager == null)
        {
            Debug.LogError("ZombieManager not found in the scene.");
        }
    }

    private void Update()
    {
        Move();

        // Check if the player is moving
        ZombieSpeedManager.SetMoving(_joystick.Horizontal != 0 || _joystick.Vertical != 0);

        // Update the speed based on whether the player is moving
        ZombieSpeedManager.IncreaseSpeed(Time.deltaTime);
        
        // Use the current speed for zombie movement
        speed = ZombieSpeedManager.currentSpeed;

        // Call the manager function to move all the in game play zombies
        if (zombieManager != null)
        {
            zombieManager.MoveAllZombies();
        }

    }

    // Method to handle player movement
    public void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal;
        _moveVector.z = _joystick.Vertical;

        // If there's joystick input, rotate the player and play the run animation
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            // Set the boolean true
            IsMoving = true;

            Vector3 direction = new Vector3(_moveVector.x, 0, _moveVector.z).normalized;
            Vector3 targetVelocity = direction * ZombieSpeedManager.currentSpeed;

            // Clamp the velocity to ensure it doesn't exceed the maximum speed
            targetVelocity = Vector3.ClampMagnitude(targetVelocity, speed);

            // Apply the target velocity to the Rigidbody
            _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

            // Rotate the player towards the movement direction
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            _animatorController.PlayRun(); // Play the run animation
        }
        // If there's no joystick input, play the idle animation
        else
        {
            // Set the boolean false
            IsMoving = false;

            _animatorController.PlayIdle(); // Play the idle animation

            // Stop the player's movement
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        }
    }

    public Vector3 GetMoveVector()
    {
        return _moveVector;
    }

}
