using UnityEngine;

public class PlayerMovement : MonoBehaviour, ICharacter
{
    [SerializeField] private FloatingJoystick _joystick; // Reference to the floating joystick
    [SerializeField] private ZombieAnimatorController zombieAnimatorController; // Reference to the animator controller
    [SerializeField] private float _rotateSpeed; // Rotation speed of the player
    [SerializeField] private float _moveSpeed; // Movement speed of the player

    private Rigidbody _rigidbody; // Reference to the Rigidbody component
    private Vector3 _moveVector; // Vector for storing movement input
    public bool isMoving; // Boolean to keep track of movement for speed

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Initialize();
        isMoving = false;
    }

    private void Update()
    {
        Move();
    }

    // Method to handle player movement
    public void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal;
        _moveVector.z = _joystick.Vertical;

        // Update the InputManager with the player's input
        if (PlayerInputManager.Instance != null)
        {
            PlayerInputManager.Instance.UpdateMoveVector(_moveVector);
        }
        else
        {
            Debug.LogError("PlayerInputManager Instance is null.");
        }

        // If there's joystick input, rotate the player and play the run animation
        if (_moveVector.magnitude > 0)
        {
            // Set the boolean true
            isMoving = true;

            Vector3 direction = new Vector3(_moveVector.x, 0, _moveVector.z).normalized;
            Vector3 targetVelocity = direction * _moveSpeed;

            // Clamp the velocity to ensure it doesn't exceed the maximum speed
            targetVelocity = Vector3.ClampMagnitude(targetVelocity, _moveSpeed);

            // Apply the target velocity to the Rigidbody
            _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

            // Rotate the player towards the movement direction
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            zombieAnimatorController.PlayRun(); // Play the run animation
        }
        else
        {
            // Set the boolean false
            isMoving = false;

            zombieAnimatorController.PlayIdle(); // Play the idle animation

            // Stop the player's movement
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
// using UnityEngine;
// // Ustteki kod ile playermovement istedigim gibi calisyor.
// public class PlayerMovement : MonoBehaviour, ICharacter
// {
//     [SerializeField] private FloatingJoystick _joystick; // Reference to the floating joystick
//     [SerializeField] private ZombieAnimatorController zombieAnimatorController; // Reference to the animator controller
//     [SerializeField] private float _rotateSpeed; // Rotation speed of the player
//     [SerializeField] private ZombieSpeedManager zombieSpeedManager; 
//     private float speed;
//     //[SerializeField] private float _moveSpeed; // Movement speed of the player

//     private Rigidbody _rigidbody; // Reference to the Rigidbody component
//     private Vector3 _moveVector; // Vector for storing movement input
//     public bool isMoving; // Boolean to keep track of movement for speed

//     public void Initialize()
//     {
//         _rigidbody = GetComponent<Rigidbody>();
//     }

//     private void Start()
//     {
//         Initialize();
//         isMoving = false;
//         if(zombieSpeedManager != null)
//         {
//             speed = zombieSpeedManager.currentSpeed;
//             Debug.Log("Speed is " + speed);
//         }
//     }

//     private void Update()
//     {
//         Move();
//     }

//     // Method to handle player movement
//     public void Move()
//     {
//         _moveVector = Vector3.zero;
//         _moveVector.x = _joystick.Horizontal;
//         _moveVector.z = _joystick.Vertical;

//         // Update the InputManager with the player's input
//         if (PlayerInputManager.Instance != null)
//         {
//             PlayerInputManager.Instance.UpdateMoveVector(_moveVector);
//         }
//         else
//         {
//             Debug.LogError("PlayerInputManager Instance is null. Ensure PlayerInputManager is properly initialized.");
//         }

//         // If there's joystick input, rotate the player and play the run animation
//         if (_moveVector.magnitude > 0)
//         {
//             // Set the boolean true
//             isMoving = true;

//             Vector3 direction = new Vector3(_moveVector.x, 0, _moveVector.z).normalized;
//             Vector3 targetVelocity = direction * speed;

//             // Clamp the velocity to ensure it doesn't exceed the maximum speed
//             targetVelocity = Vector3.ClampMagnitude(targetVelocity, speed);

//             // Apply the target velocity to the Rigidbody
//             _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

//             // Rotate the player towards the movement direction
//             Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, _rotateSpeed * Time.deltaTime, 0.0f);
//             transform.rotation = Quaternion.LookRotation(newDirection);

//             zombieAnimatorController.PlayRun(); // Play the run animation
//         }
//         else
//         {
//             // Set the boolean false
//             isMoving = false;

//             zombieAnimatorController.PlayIdle(); // Play the idle animation

//             // Stop the player's movement
//             _rigidbody.velocity = Vector3.zero;
//         }
//     }
// }
