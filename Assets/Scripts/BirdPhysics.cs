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

    const int MAXLIVES = 5;
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
        if (Input.GetMouseButtonDown(0)) 
        {
            // Flap cap amunt
            birdBody.AddForce(Vector2.up * force);
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
}
