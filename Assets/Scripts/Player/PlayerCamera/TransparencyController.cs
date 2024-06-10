using UnityEngine;

// Makes the object transparent to see the player.
public class TransparencyController : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public float maxDistance = 10f;
    public float alphaValue = 0.4f;
    public float lerpSpeed;

    private Material material;
    private float targetAlpha;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            targetAlpha = material.color.a;
        }
        else
        {
            Debug.LogError("Renderer component not found on object.");
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 direction = (player.position - mainCamera.transform.position).normalized;

        if (Physics.Raycast(mainCamera.transform.position, direction, out hit, maxDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                targetAlpha = alphaValue;
                Debug.Log("Object is between player and camera. Setting transparency to " + alphaValue);
            }
        }
        else
        {
            targetAlpha = 1f;
            Debug.Log("No object detected between player and camera. Setting transparency to 1");
        }

        Color color = material.color;
        color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * lerpSpeed);
        material.color = color;
    }
}
