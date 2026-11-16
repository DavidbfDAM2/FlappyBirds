using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject butterflyPrefab;
    
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float spawnTime;

    [SerializeField]
    BirdPhysics playerBird;
    
    // Inicialitzem els components abans de la primera actualització.    
    void Start()
    {
        InvokeRepeating(nameof(SpawnButterfly), 2, spawnTime);      // intents periodics de generar papallones, començant al cap de dos segons de joc
    }

    // Executem aquestes comprovacions a cada fotograma.
    void Update()
    {
        
    }

    void SpawnButterfly() 
    {
        float minY, maxY, randomY, rightEdge;
        Vector3 spawnPosition;

        if (playerBird.NeedsHealing()) // creem una papallona davant de la camera a una altura aleatoria (en cas que el jugador no tingui el maxim de vides)
        {
            minY = mainCamera.transform.position.y - mainCamera.orthographicSize + 1.5f;
            maxY = mainCamera.transform.position.y + mainCamera.orthographicSize - 1.5f;
            randomY = Random.Range(minY, maxY);

            rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
            spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
            Instantiate(butterflyPrefab, spawnPosition, Quaternion.identity);
        }        
    }
}