using UnityEngine;
using System.Collections;

public class BirdPhysics : MonoBehaviour
{
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
    
    [SerializeField]
    AudioClip flapSound;

    [SerializeField]
    AudioClip hitSound;

    [SerializeField]
    AudioClip healSound;

    [SerializeField]
    AudioClip pickupSound;
    AudioSource audioSource;

    [SerializeField]
    GameSceneController sceneController;
    
    SpriteRenderer birdRenderer;
    bool isBlinking;
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
    {   audioSource = GetComponent<AudioSource>();        // obte els components de locell i li asigna una velocitat      
        birdRenderer = GetComponent<SpriteRenderer>();    
        birdBody = GetComponent<Rigidbody2D>();
        birdBody.linearVelocity = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()   
    {
        if (sceneController.IsPlaying)        // gestionem els controls
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Flap cap amunt
                birdBody.AddForce(Vector2.up * force);
                audioSource.PlayOneShot(flapSound);
            }

            horizontalInput = 0f;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                horizontalInput = horizontalInput + 1f;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                horizontalInput = horizontalInput - 1f;
            }
        }
    }

    void FixedUpdate()                      // apliquem la velocitat horitzontal seleccionada sense alterar la velocitat vertical
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

    private void OnCollisionEnter2D(Collision2D collision)      // acabem la partida amb una colisio + reproduim el so de limpacte
    {
        if (sceneController.IsPlaying)
        {
            sceneController.ShowGameOver();
            audioSource.PlayOneShot(hitSound);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)     // gestionem la recollida de vides i punts i la colisio amb enemics
    {
        if (sceneController.IsPlaying)
        {
            if (collision.gameObject.GetComponent<ButterflyPhysics>())
            {
                Heal();
                Destroy(collision.gameObject);
            }

            EnemyBirdPhysics enemy = collision.GetComponentInParent<EnemyBirdPhysics>();

            if (enemy != null)
            {
                TakeDamage();
                collision.enabled = false;
                enemy.ReceiveHit();
            }
            if (sceneController.IsPlaying && collision.gameObject.CompareTag("ScorePickup"))
            {
                AddScore();
                Destroy(collision.gameObject);
            }
        }
    }

    void TakeDamage()               
    {
        currentLives--;                     // restem una vida, reprodueim limpacte i activa el parpelleig o el 'game over'
        audioSource.PlayOneShot(hitSound);

        if(currentLives > 0 && !isBlinking) 
        {
            StartCoroutine(BlinkAfterHit());
        }

        if (currentLives <= 0)
        {
            sceneController.ShowGameOver();
        }
    }
    
    void Heal()     
    {
        if(currentLives < MAXLIVES)         // recuperem una vida i reprodueim el so de curacio si NO hem arribat al maxim de vides
        {
            currentLives++;
            audioSource.PlayOneShot(healSound);
        }
    }
    
    public bool NeedsHealing()              // indica si locell te menys vides que el maxim permes
    {
        return currentLives < MAXLIVES;
    }

    void AddScore()                         // sumem deu punts i reprodueim el so de recollida del 'pickup'
    {
        score = score + 10;
        audioSource.PlayOneShot(pickupSound);
    }
    
    public void RegisterPassedObstacle()     // incrementem el comptador dobstacles superats
    {
        if (sceneController.IsPlaying)
        {
            obstaclePassed++;
        }
    }
        
    IEnumerator BlinkAfterHit()             // corutina; parpalleig tres vegades despres dun impacte, sense alterar la fisica; alternem la visibilitat tres vegades amb esperes de temps real
    {
        isBlinking = true;

        for (int i = 0; i < 3; i++)
        {
            birdRenderer.enabled = false;
            yield return new WaitForSecondsRealtime(0.1f);

            birdRenderer.enabled = true;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        isBlinking = false;
    }
}