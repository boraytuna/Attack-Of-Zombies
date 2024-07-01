using UnityEngine;

public class GamePlayZombieMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private ZombieAnimatorController zombieAnimatorController;

    private Transform playerTransform;
    private float moveSpeed = 10f; // Movement speed of the zombie
    private float rotateSpeed = 6f;
    private float catchUpSpeed = 15f; // Speed when catching up to the player
    private float followRadius = 3f; // Radius around the player to follow input
    private bool isFollowingInput = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        zombieAnimatorController = GetComponent<ZombieAnimatorController>();

        // Initialize playerTransform with the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > followRadius)
        {
            // If outside the follow radius, catch up to the player
            isFollowingInput = false;
            MoveTowardsPlayer();
        }
        else
        {
            // If inside the follow radius, follow player input
            isFollowingInput = true;
            Move();
        }
    }

    private void Move()
    {
        if (isFollowingInput)
        {
            Vector3 moveVector = PlayerInputManager.Instance.MoveVector;

            if (moveVector.magnitude > 0)
            {
                Vector3 direction = new Vector3(moveVector.x, 0, moveVector.z).normalized;
                Vector3 targetVelocity = direction * moveSpeed;

                // Clamp the velocity to ensure it doesn't exceed the maximum speed
                targetVelocity = Vector3.ClampMagnitude(targetVelocity, moveSpeed);

                // Apply the target velocity to the Rigidbody
                _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

                // Rotate the zombie towards the movement direction
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);

                // Play the run animation
                zombieAnimatorController.PlayRun();
            }
            else
            {
                // Play the idle animation
                zombieAnimatorController.PlayIdle();

                // Stop the zombie's movement
                _rigidbody.velocity = Vector3.zero;
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Vector3 targetVelocity = direction * catchUpSpeed;

        // Apply the target velocity to the Rigidbody
        _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

        // Rotate the zombie towards the player
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        // Play the run animation
        zombieAnimatorController.PlayRun();
    }

}
// using UnityEngine;

// public class GamePlayZombieMovement : MonoBehaviour
// {
//     private Rigidbody _rigidbody;
//     private ZombieAnimatorController zombieAnimatorController;

//     [SerializeField] private ZombieSpeedManager zombieSpeedManager; 
//     private float speed;

//     [SerializeField]
//     private GameObject player;
//     private PlayerMovement playerMovement;

//     private Transform playerTransform;
//     //private float moveSpeed = 10f; // Movement speed of the zombie
//     private float rotateSpeed = 6f;
//     private float catchUpSpeed = 15f; // Speed when catching up to the player
//     private float followRadius = 3f; // Radius around the player to follow input
//     private bool isFollowingInput = true;

//     private void Start()
//     {
//         _rigidbody = GetComponent<Rigidbody>();
//         zombieAnimatorController = GetComponent<ZombieAnimatorController>();

//         // Initialize playerTransform with the player's transform
//         playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

//         if (player != null)
//         {
//             // Find the Playermovement script
//             if (player != null)
//             {
//                 playerMovement = player.GetComponent<PlayerMovement>();
//             }
//         }

//     }

//     private void Update()
//     {
//         float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

//         if(zombieSpeedManager != null && playerMovement.isMoving == true)
//         {
//             speed = zombieSpeedManager.currentSpeed;
//         }

//         if (distanceToPlayer > followRadius)
//         {
//             // If outside the follow radius, catch up to the player
//             isFollowingInput = false;
//             MoveTowardsPlayer();
//         }
//         else
//         {
//             // If inside the follow radius, follow player input
//             isFollowingInput = true;
//             Move();
//         }
//     }

//     private void Move()
//     {
//         if (isFollowingInput)
//         {
//             Vector3 moveVector = PlayerInputManager.Instance.MoveVector;

//             if (moveVector.magnitude > 0)
//             {
//                 Vector3 direction = new Vector3(moveVector.x, 0, moveVector.z).normalized;
//                 Vector3 targetVelocity = direction * speed;

//                 // Clamp the velocity to ensure it doesn't exceed the maximum speed
//                 targetVelocity = Vector3.ClampMagnitude(targetVelocity, speed);

//                 // Apply the target velocity to the Rigidbody
//                 _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

//                 // Rotate the zombie towards the movement direction
//                 Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f);
//                 transform.rotation = Quaternion.LookRotation(newDirection);

//                 // Play the run animation
//                 zombieAnimatorController.PlayRun();
//             }
//             else
//             {
//                 // Play the idle animation
//                 zombieAnimatorController.PlayIdle();

//                 // Stop the zombie's movement
//                 _rigidbody.velocity = Vector3.zero;
//             }
//         }
//     }

//     private void MoveTowardsPlayer()
//     {
//         Vector3 direction = (playerTransform.position - transform.position).normalized;
//         Vector3 targetVelocity = direction * catchUpSpeed;

//         // Apply the target velocity to the Rigidbody
//         _rigidbody.velocity = new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.z);

//         // Rotate the zombie towards the player
//         Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f);
//         transform.rotation = Quaternion.LookRotation(newDirection);

//         // Play the run animation
//         zombieAnimatorController.PlayRun();
//     }

// }
