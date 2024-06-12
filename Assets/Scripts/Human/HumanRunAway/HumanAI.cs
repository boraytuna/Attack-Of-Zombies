using UnityEngine;
using UnityEngine.AI;

// This script moves the human objects using nav mesh agent.
public class HumanAI : MonoBehaviour
{
    private static Vector3 escapePoint; // Static escape point shared by all humans
    private NavMeshAgent agent; // Reference to NavMeshAgent component
    private Transform mainHuman; // Reference to the main human of the group

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get reference to NavMeshAgent component
    }

    private void OnEnable()
    {
        SetEscapePoint(escapePoint); // Ensure escape point is set when enabled
    }

    public static void SetEscapePoint(Vector3 newEscapePoint)
    {
        escapePoint = newEscapePoint; // Set the shared escape point for all humans
    }

    // Set the main human of the group
    public void SetMainHuman(Transform mainHuman)
    {
        this.mainHuman = mainHuman; // Set the main human reference
    }

    // Check if this human is the main human of the group
    public bool IsMainHuman(Transform human)
    {
        return human == mainHuman; // Check if provided human is the main human
    }

    public void MoveToEscapePoint()
    {
        if (agent != null)
        {
            agent.SetDestination(escapePoint); // Move towards the shared escape point
        }
    }
}
