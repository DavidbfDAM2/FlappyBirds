using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdPhysics : MonoBehaviour
{
    // Bird's health points
    [SerializeField]
    int currentLives = 5;
    
    //Movement speed 
    [SerializeField]
    float speed = 2.0f;

    //Flap force
    [SerializeField]
    float force = 200.0f;
    
    [SerializeField]
    float slowSpeed = 1.0f;

    [SerializeField]
    float fastSpeed = 3.0f;

    float horizontalInput;
    const int MAXLIVES = 5;
    int score = 0;
    int obstaclePassed = 0;
    public float HorizontalInput
    {
        get { return horizontalInput; }
    }
    public int CurrentLives
    {
        get { return this.currentLives; }
    }
    public int Score 
    {
        get { return this.score; }
    }
    public int ObstaclesPassed 
    {
        get { return obstaclePassed; }
    }
    Rigidbody2D birdBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        birdBody = GetComponent<Rigidbody2D>();
        birdBody.linearVelocity = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            // Flap cap amunt
            birdBody.AddForce(Vector2.up * force);
        }
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput += 1f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput -= 1f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ButterflyPhysics>()) 
        {
            Heal();
            Destroy(collision.gameObject);
        }
        
        EnemyBirdPhysics enemy = collision.GetComponentInParent<EnemyBirdPhysics>();
        if(enemy != null)
        {
            TakeDamage();
            collision.enabled = false;
            enemy.ReceiveHit();
        }
        if (collision.gameObject.GetComponent<ScorePickup>()) 
        {
            AddScore();
            Destroy(collision.gameObject);
        }

    }
    void TakeDamage() 
    {
        currentLives--;
        if(currentLives <= 0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        Debug.Log(currentLives);
    }
    void Heal() 
    {
        if(currentLives < MAXLIVES) 
        {
            currentLives++;
        }
    }
    public bool NeedsHealing() 
    {
        return currentLives < MAXLIVES;
    }

    void AddScore() 
    {
        score = score + 10;
        Debug.Log(score);
    }
    public void RegisterPassedObstacle() 
    {
        obstaclePassed++;
        Debug.Log(obstaclePassed);
    }
    void FixedUpdate()
    {
        float targetSpeed = speed;

        if (horizontalInput > 0f)
        {
            targetSpeed = fastSpeed;
        }
        else if (horizontalInput < 0f)
        {
            targetSpeed = slowSpeed;
        }

        birdBody.linearVelocity = new Vector2(targetSpeed, birdBody.linearVelocity.y);
    }
}
