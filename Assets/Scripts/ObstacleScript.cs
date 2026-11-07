using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;
    
    [SerializeField]
    float timeBetweenReverse = 0f;

    Rigidbody2D obstacleBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Assign velocity at obstacle's rigidbody
        obstacleBody = GetComponent<Rigidbody2D>();
        obstacleBody.linearVelocity = Vector2.up * speed;

        //Switch velocity every timeBetweenReverse
        InvokeRepeating("Reverse", 0, timeBetweenReverse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reverse() 
    {
        // Invertim la direcció de la velocitat. És com canviar de sentit
        obstacleBody.linearVelocity *= -1;
    }
}
