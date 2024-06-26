using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPointer : MonoBehaviour
{
    public string humanTag = "Human"; // Tag used to identify human objects
    public Transform playerTransform; // Reference to the player transform
    public RectTransform arrowTransform; // Reference to the arrow UI element

    void Start()
    {
        // If playerTransform is not set, try to find it
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (arrowTransform == null)
        {
            Debug.LogError("Arrow Transform is not set.");
        }
    }

    void Update()
    {
        Transform closestHuman = FindClosestHuman();
        if (closestHuman != null)
        {
            RotateArrowTowards(closestHuman.position);
        }
        else
        {
            Debug.Log("No human objects found.");
        }
    }

    Transform FindClosestHuman()
    {
        GameObject[] humans = GameObject.FindGameObjectsWithTag(humanTag); // Find all human objects
        Transform closestHuman = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject human in humans)
        {
            float distance = Vector3.Distance(playerTransform.position, human.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHuman = human.transform;
            }
        }

        return closestHuman;
    }

    void RotateArrowTowards(Vector3 targetPosition)
    {
        // Calculate the direction from the player to the target
        Vector3 direction = targetPosition - playerTransform.position;
        direction.y = 0; // Ignore the vertical component

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Rotate the arrow UI element
        arrowTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
}
