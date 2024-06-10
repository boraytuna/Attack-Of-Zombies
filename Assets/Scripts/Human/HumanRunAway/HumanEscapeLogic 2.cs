// using UnityEngine;
// using UnityEngine.AI;

// // This script calculates an escape point in the map away from the zombies.
// public class HumanEscapeLogic : MonoBehaviour
// {
//     private HumanDetection humanDetection; // Reference to the HumanDetection script on the same GameObject
//     private ZombieDetection zombieDetection;
//     private float escapeRange = 10f; // Range within which to calculate the escape point
//     private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

//     private Vector3 escapePoint; // Current escape point for the group of humans

//     void Start()
//     {
//         // Get the HumanDetection script and NavMeshAgent component on the same GameObject
//         humanDetection = GetComponent<HumanDetection>();
//         navMeshAgent = GetComponent<NavMeshAgent>();
//         zombieDetection = GetComponent<ZombieDetection>();

//         // Subscribe to the event in HumanDetection for when a zombie is detected
//         humanDetection.OnZombieDetected += CalculateEscapePoint;
//     }

//     void Update()
//     {
//         // Move humans towards the escape point
//         MoveHumansToEscapePoint();
//     }

//     void CalculateEscapePoint()
//     {
//         Vector3 zombiePosition = zombieDetection.transform.position;
//         Vector3 directionToZombie = transform.position - zombiePosition;
//         Vector3 escapeDirection = directionToZombie.normalized * escapeRange;

//         // Calculate a new escape point on the opposite side of the zombie
//         Vector3 escapePosition = zombiePosition + escapeDirection;

//         // Check if the escape position is within the NavMesh
//         NavMeshHit hit;
//         if (NavMesh.SamplePosition(escapePosition, out hit, escapeRange, NavMesh.AllAreas))
//         {
//             escapePoint = hit.position;
//         }
//         else
//         {
//             // If the escape position is outside the NavMesh, move towards a random point within the escape range
//             Vector3 randomDirection = Random.insideUnitSphere * escapeRange;
//             randomDirection += transform.position;
//             NavMesh.SamplePosition(randomDirection, out hit, escapeRange, NavMesh.AllAreas);
//             escapePoint = hit.position;
//         }
//     }

//     void MoveHumansToEscapePoint()
//     {
//         // Move all humans in the group towards the escape point
//         if (zombieDetection.zombieInRange == true)
//         {
//             CalculateEscapePoint();
//             navMeshAgent.SetDestination(escapePoint);
//         }
//     }

//     void OnDestroy()
//     {
//         // Unsubscribe from the event when this object is destroyed
//         humanDetection.OnZombieDetected -= CalculateEscapePoint;
//     }
// }
