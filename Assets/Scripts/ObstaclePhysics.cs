using UnityEngine;

public class ObstaclePhysics : MonoBehaviour
{    
    [SerializeField]
    float speed;
    
    [SerializeField]
    float directionChangeInterval;
    
    Rigidbody2D obstacleBody;
    private bool obstacleCounted = false;
    private BirdPhysics playerBird;
    private Collider2D obstacleCollider;
    private Collider2D playerBirdCollider;

    // Inicialitzem els components abans de la primera actualització.
    
    void Start()
    {
        // assignem la velocitat inicial de lobstacle
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

        // programem el primer canvi de sentit i les repeticions amb el mateix interval
        InvokeRepeating(nameof(Reverse), directionChangeInterval, directionChangeInterval);
    }

    // Executem aquestes comprovacions a cada fotograma.    
    void Update()
    {
        if (!obstacleCounted && playerBirdCollider.bounds.min.x > obstacleCollider.bounds.max.x)    // comptem lobstacle una sola vegada quan el jugador lha superat horitzontalment
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