using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D objectCleaner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;    // obtenim la camera principal i el cos fisic de lobjecte netejador
        objectCleaner = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame    
    void FixedUpdate()
    {
        float leftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;     // mantenim el netejador dues unitats darrere del marge esquerre de la camera
        Vector2 targetPosition = new Vector2(leftEdge - 2f, mainCamera.transform.position.y);
        objectCleaner.MovePosition(targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   // eliminem els punts, papallones, obstacles i enemics que entren al netejador

        if (collision.gameObject.CompareTag("ScorePickup") || collision.gameObject.GetComponent<ButterflyPhysics>() || collision.gameObject.GetComponent<ObstaclePhysics>() || collision.gameObject.GetComponent<EnemyBirdPhysics>())
        {
            Destroy(collision.gameObject);
        }        
    }    
}