using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject obstaclePrefab;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float minSpawnDistance;

    [SerializeField]
    float maxSpawnDistance;

    [SerializeField]
    float spawnMargin;              // distancia de preparacio dels obstacles mes enlla del marge dret de la camera

    [SerializeField]
    float minHeightSpawn;

    [SerializeField]
    float maxHeightSpawn;   

    float nextSpawnX;
    bool isInitialized;

    // Inicialitzem els components abans de la primera actualització.    
    void Start()
    {
    }

    // Executem aquestes comprovacions a cada fotograma.
    void Update()
    {
        float rightEdge;

        if (Time.timeScale > 0f)
        {
            rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

            if (!isInitialized)            // fixem la posicio del primer obstacle quan comença la partida
            {
                nextSpawnX = rightEdge + spawnMargin;
                isInitialized = true;
            }
            
            while (nextSpawnX <= rightEdge + spawnMargin)       // generem obstacles mentre la seva posicio prevista quedi dins del marge de generacio
            {
                SpawnObstacle();
            }
        }
    }
    
    void SpawnObstacle()        // creem lobstacle a la posicio prevista i calculem la seguent
    {
        float randomY, minimumX, maximumX;
        Vector3 spawnPosition;

        randomY = Random.Range(minHeightSpawn, maxHeightSpawn);

        spawnPosition = new Vector3(nextSpawnX, randomY, 0f);

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity );

        minimumX = Mathf.Max(0.1f, minSpawnDistance);        // assignem una separacio minima positiva per evitar un bucle infinit
        maximumX = Mathf.Max(minimumX, maxSpawnDistance);

        nextSpawnX = nextSpawnX + Random.Range(minimumX, maximumX);     // mesurem la separacio entre posicions X dels obstacles
    }
}