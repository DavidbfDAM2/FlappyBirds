using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D objectCleaner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        objectCleaner = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float leftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

        Vector2 targetPosition = new Vector2(leftEdge - 2f, mainCamera.transform.position.y);

        objectCleaner.MovePosition(targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (    collision.GetComponent<ScorePickup>() != null 
            ||  collision.GetComponent<ButterflyPhysics>() 
            ||  collision.GetComponent<ObstaclePhysics>()
            ||  collision.GetComponent<EnemyBirdPhysics>())
        {
            Destroy(collision.gameObject);
        }        
    }    
}
