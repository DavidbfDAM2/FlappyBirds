using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdPhysics : MonoBehaviour
{
    //Movement speed 
    [SerializeField]
    float speed = 2.0f;

    //Flap force
    [SerializeField]
    float force = 200.0f;

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
}
