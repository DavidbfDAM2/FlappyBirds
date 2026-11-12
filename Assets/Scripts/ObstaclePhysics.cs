using UnityEngine;

public class ObstaclePhysics : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;
    
    [SerializeField]
    float timeBetweenReverse = 0f;

    Rigidbody2D obstacleBody;
    private Camera mainCamera;
    private bool obstacleCounted = false;
    private BirdPhysics playerBird;
    private Collider2D obstacleCollider;
    private Collider2D playerBirdCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        // Assign velocity at obstacle's rigidbody
        obstacleBody = GetComponent<Rigidbody2D>();
        obstacleBody.linearVelocity = Vector2.up * speed;
        
        obstacleCollider = GetComponent<Collider2D>();

        playerBird = FindFirstObjectByType<BirdPhysics>();
        playerBirdCollider = playerBird.GetComponent<Collider2D>();

        //Switch velocity every timeBetweenReverse
        InvokeRepeating("Reverse", 0, timeBetweenReverse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!obstacleCounted && playerBirdCollider.bounds.min.x > obstacleCollider.bounds.max.x ) 
        {
            playerBird.RegisterPassedObstacle();
            obstacleCounted = true;
        }
        float leftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

        if (transform.position.x < leftEdge - 1f)
        {
            Destroy(gameObject);
        }
    }

    void Reverse() 
    {
        // Invertim la direcció de la velocitat. És com canviar de sentit
        obstacleBody.linearVelocity *= -1;
    }
}
