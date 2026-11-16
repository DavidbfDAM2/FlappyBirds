using UnityEngine;

public class EnemyBirdPhysics : MonoBehaviour
{
    //Movement speed 
    [SerializeField]
    float speed;
    
    [SerializeField]
    float enemyActivationDistance;
    
    [SerializeField]
    float hitImpulse = 3f;
    
    Rigidbody2D enemyBody;
    private BirdPhysics playerBird;
    private bool isActivated = false;
    private Camera mainCamera;
    
    // Inicialitzem els components abans de la primera actualització.
    void Start()
    {
        mainCamera = Camera.main;                           // localitzem la camera, el jugador i el cos fisic de lenemic
        enemyBody = GetComponent<Rigidbody2D>();
        playerBird = FindFirstObjectByType<BirdPhysics>();
    }

    // Executem aquestes comprovacions a cada fotograma.
    void Update()
    {
        float distance, bottomEdge;

        distance = Vector2.Distance(transform.position, playerBird.transform.position);     // activem lenemic quan el jugador sacosta + eliminem lenemic si cau per sota de la pantalla (si lenemic no colisiona amb el jugador i surt per lesquerra de la pantalla ja el nateja 'ObjectCleaner')
                
        if(!isActivated && distance <= enemyActivationDistance) 
        {
            isActivated = true;
            enemyBody.linearVelocity = Vector2.left * speed;
        }
        
        bottomEdge = mainCamera.transform.position.y - mainCamera.orthographicSize;

        if (transform.position.y < bottomEdge - 1f)
        {
            Destroy(gameObject);
        }
    }
    
    public void ReceiveHit() // activem la gravetat de lenemic i limpulsem cap a la dreta despres de la colisio
    {
        isActivated = true;
        enemyBody.gravityScale = 1f;
        enemyBody.linearVelocity = Vector2.zero;
        enemyBody.AddForce(Vector2.right * hitImpulse, ForceMode2D.Impulse);
    }
}