using UnityEngine;

public class EnemyBirdPhysics : MonoBehaviour
{
    //Movement speed 
    [SerializeField]
    float speed = 2.0f;
    
    [SerializeField]
    float enemyActivationDistance = 7.0f;
    
    [SerializeField]
    float hitImpulse = 3f;
    
    Rigidbody2D enemyBody;
    private BirdPhysics playerBird;
    private bool isActivated = false;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        enemyBody = GetComponent<Rigidbody2D>();
        playerBird = FindFirstObjectByType<BirdPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerBird.transform.position); 
        
        if(!isActivated && distance <= enemyActivationDistance) 
        {
            isActivated = true;
            enemyBody.linearVelocity = Vector2.left * speed;
        }
        
        float bottomEdge = mainCamera.transform.position.y - mainCamera.orthographicSize;

        if (transform.position.y < bottomEdge - 1f)
        {
            Destroy(gameObject);
        }
    }
    public void ReceiveHit() 
    {
        isActivated = true;
        enemyBody.gravityScale = 1f;
        enemyBody.linearVelocity = Vector2.zero;
        enemyBody.AddForce(Vector2.right * hitImpulse, ForceMode2D.Impulse);
    }
}
