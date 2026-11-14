using UnityEngine;

public class ObstaclePhysics : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;
    
    [SerializeField]
    float directionChangeInterval = 2f;

    Rigidbody2D obstacleBody;
    private bool obstacleCounted = false;
    private BirdPhysics playerBird;
    private Collider2D obstacleCollider;
    private Collider2D playerBirdCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        // Assign velocity at obstacle's rigidbody
        obstacleBody = GetComponent<Rigidbody2D>();         // iniciem el moviment vertical, obtenim els colisionadors i programem els canvis de sentit
        
        if (Random.Range(0, 2) == 0)
        {
            obstacleBody.linearVelocity = Vector2.up * speed;
        }
        else
        {
            obstacleBody.linearVelocity = Vector2.down * speed;
        }

        obstacleCollider = GetComponent<Collider2D>();
        playerBird = FindFirstObjectByType<BirdPhysics>();
        playerBirdCollider = playerBird.GetComponent<Collider2D>();

        //Switch velocity every timeBetweenReverse
        InvokeRepeating("Reverse", directionChangeInterval, directionChangeInterval);
    }

    // Update is called once per frame    
    void Update()
    {
        if (!obstacleCounted && playerBirdCollider.bounds.min.x > obstacleCollider.bounds.max.x)    // compta lobstacle una sola vegada quan el jugador lha superat horitzontalment
        {
            playerBird.RegisterPassedObstacle();
            obstacleCounted = true;
        }        
    }

    void Reverse()     
    {
        // Invertim la direcció de la velocitat. És com canviar de sentit
        obstacleBody.linearVelocity *= -1;
    }    
}