using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

// This script spawns humans in the map with the prefered numbers.
public class HumanSpawner : MonoBehaviour
{
    public GameObject humanPrefab;
    public Transform zombie; // Reference to the zombie object

    [Range(1, 20)]
    public float minDistanceFromZombie; // Minimum distance from the zombie
    [Range(1, 100)]
    public int minNumberOfGroups;
    [Range(1, 100)]
    public int maxNumberOfGroups;
    [Range(1, 100)]
    public int minHumansPerGroup;
    [Range(1, 100)]
    public int maxHumansPerGroup;
    [Range(1f, 100)]
    public float minGroupRadius;
    [Range(1f, 100f)]
    public float maxGroupRadius;
    [Range(1f, 100f)]
    public float minGroupSeparationDistance; // Minimum distance between groups

    public NavMeshSurface navMeshSurface;

    private List<Vector3> groupCenters = new List<Vector3>();

    void Start()
    {
        navMeshSurface.BuildNavMesh(); // Ensure the NavMesh is built before spawning humans

        int numberOfGroups = Random.Range(minNumberOfGroups, maxNumberOfGroups + 1);
        for (int i = 0; i < numberOfGroups; i++)
        {
            Vector3 groupCenter = GetRandomSpawnPositionOnNavMesh();
            int humansPerGroup = Random.Range(minHumansPerGroup, maxHumansPerGroup + 1);
            float groupRadius = Random.Range(minGroupRadius, maxGroupRadius);

            // Check if the group is too close to any existing group
            bool tooClose = false;
            foreach (Vector3 existingCenter in groupCenters)
            {
                if (Vector3.Distance(groupCenter, existingCenter) < minGroupSeparationDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            // If too close, find a new position
            if (tooClose)
            {
                i--; // Try again with the same index
                continue;
            }

            groupCenters.Add(groupCenter);
            SpawnGroup(groupCenter, humansPerGroup, groupRadius);
        }
    }

    void SpawnGroup(Vector3 center, int humansPerGroup, float groupRadius)
    {
        for (int i = 0; i < humansPerGroup; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * groupRadius;
            randomOffset.y = 0; // Ensure humans spawn on the NavMesh surface
            Vector3 spawnPosition = center + randomOffset;

            // Check if the spawn position is far enough from the zombie
            while (Vector3.Distance(spawnPosition, zombie.position) < minDistanceFromZombie)
            {
                spawnPosition = GetRandomSpawnPositionOnNavMesh(); // Get a new random position
            }

            // Check if the spawn position is on the NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                // Spawn a human at the valid position
                Instantiate(humanPrefab, hit.position, Quaternion.identity);
            }
            else
            {
                // If the position is not on the NavMesh, try again with a new random position
                i--; // Try again with the same index
            }
        }
    }

    Vector3 GetRandomSpawnPositionOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick a random triangle from the NavMesh
        int randomTriangleIndex = Random.Range(0, navMeshData.indices.Length / 3);
        Vector3 randomPoint = (
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3]] +
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3 + 1]] +
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3 + 2]]
        ) / 3f;

        return randomPoint;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach (Vector3 groupCenter in groupCenters)
        {
            Gizmos.DrawWireSphere(groupCenter, minGroupSeparationDistance);
        }
    }
}
