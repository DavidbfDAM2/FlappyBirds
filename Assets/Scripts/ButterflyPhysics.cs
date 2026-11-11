using UnityEngine;

public class ButterflyPhysics : MonoBehaviour
{
    //Movement speed 
    [SerializeField]
    float speed = 2.0f;
    
    Rigidbody2D butterflyBody;
    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        butterflyBody = GetComponent<Rigidbody2D>();
        butterflyBody.linearVelocity = Vector2.left * speed;
    }

    // Update is called once per frame
    void Update()
    {
        float leftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        if (transform.position.x < leftEdge - 1f)
        {
            Destroy(gameObject);
        }
    }
}