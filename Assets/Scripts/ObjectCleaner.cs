using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D objectCleaner;

    // Inicialitzem els components abans de la primera actualització.
    void Start()
    {
        mainCamera = Camera.main;    // obtenim la camera principal i el cos fisic de lobjecte netejador
        objectCleaner = GetComponent<Rigidbody2D>();        
    }

    // actualitzem la posicio del netejador a cada pas de fisica    
    void FixedUpdate()
    {
        float leftEdge;
        Vector2 targetPosition;

        leftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;     // mantenim el netejador dues unitats darrere del marge esquerre de la camera
        
        targetPosition = new Vector2(leftEdge - 2f, mainCamera.transform.position.y);
        objectCleaner.MovePosition(targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   // eliminem els pickups de puntuacio, papallones, obstacles i enemics que entren al netejador

        if (collision.gameObject.CompareTag("ScorePickup") || collision.gameObject.GetComponent<ButterflyPhysics>() || collision.gameObject.GetComponent<ObstaclePhysics>() || collision.gameObject.GetComponent<EnemyBirdPhysics>())
        {
            Destroy(collision.gameObject);
        }        
    }    
}