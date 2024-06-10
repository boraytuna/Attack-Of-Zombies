// using UnityEngine;

// // This script is responsible for moving the zombie player.
// [RequireComponent(typeof(Rigidbody))]
// public class PlayerMovement : MonoBehaviour, ICharacter
// {
//     [SerializeField] private FloatingJoystick _joystick; // Reference to the floating joystick
//     [SerializeField] private AnimatorController _animatorController; // Reference to the animator controller
//     [SerializeField] private float _rotateSpeed; // Rotation speed of the player

//     private Rigidbody _rigidbody; // Reference to the Rigidbody component
//     private Vector3 _moveVector; // Vector for storing movement input

//     private void Awake()
//     {
//         _rigidbody = GetComponent<Rigidbody>();
//     }

//     private void Update()
//     {
//         Move(); 
//         ZombieSpeedManager.IncreaseSpeed(Time.deltaTime); 
//     }
    

//     // Method to handle player movement
//     public void Move()
//     {
//         _moveVector = Vector3.zero; 
//         _moveVector.x = _joystick.Horizontal * ZombieSpeedManager.currentSpeed * Time.deltaTime;
//         _moveVector.z = _joystick.Vertical * ZombieSpeedManager.currentSpeed * Time.deltaTime;

//         // If there's joystick input, rotate the player and play the run animation
//         if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
//         {
//             Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
//             transform.rotation = Quaternion.LookRotation(direction); // Rotate the player towards the movement direction

//             _animatorController.PlayRun(); // Play the run animation
//         }
//         // If there's no joystick input, play the idle animation
//         else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
//         {
//             _animatorController.PlayIdle(); // Play the idle animation
//         }

//         _rigidbody.MovePosition(_rigidbody.position + _moveVector);
//     }
// }

using UnityEngine;
public class PlayerMovement : MonoBehaviour, ICharacter
{
    [SerializeField] private FloatingJoystick _joystick; // Reference to the floating joystick
    [SerializeField] private AnimatorController _animatorController; // Reference to the animator controller
    [SerializeField] private float _rotateSpeed; // Rotation speed of the player

    private Rigidbody _rigidbody; // Reference to the Rigidbody component
    private Vector3 _moveVector; // Vector for storing movement input

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        ZombieSpeedManager.IncreaseSpeed(Time.deltaTime);
    }

    // Method to handle player movement
    public void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal * ZombieSpeedManager.currentSpeed * Time.deltaTime;
        _moveVector.z = _joystick.Vertical * ZombieSpeedManager.currentSpeed * Time.deltaTime;

        // If there's joystick input, rotate the player and play the run animation
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction); // Rotate the player towards the movement direction

            _animatorController.PlayRun(); // Play the run animation
        }
        // If there's no joystick input, play the idle animation
        else
        {
            _animatorController.PlayIdle(); // Play the idle animation
        }

        _rigidbody.MovePosition(_rigidbody.position + _moveVector);
    }

}