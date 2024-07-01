using UnityEngine;
// Daha zombileri zamanla hizlandiracak methodu yazmadim.
public class ZombieSpeedManager : MonoBehaviour
{
    public static ZombieSpeedManager Instance { get; private set; }

    [SerializeField]
    private GameObject player;
    private PlayerMovement playerMovement;

    public float currentSpeed;
    private float minSpeed = 10f;
    private float middleSpeed = 10f;
    private float maxSpeed = 15f;
    private float speedIncreaseRate = 1f;

    private bool reachedMaxSpee = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentSpeed = minSpeed;

        if (player != null)
        {
            // Find the Playermovement script
            if (player != null)
            {
                playerMovement = player.GetComponent<PlayerMovement>();
            }
        }
    }

    private void Update()
    {
        if(playerMovement.isMoving == true)
        {
            Debug.Log("Player Moving");
        }
        else
        {
            Debug.Log("Player not Moving");
        }
    }

}
