using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float minSpawnTime;

    [SerializeField]
    float maxSpawnTime;
    
    [SerializeField]
    Transform playerBird;

    // Inicialitzem els components abans de la primera actualització.
    void Start()
    {
        Invoke(nameof(SpawnEnemy), 3f);
    }

    // Executem aquestes comprovacions a cada fotograma.
    void Update()
    {
    }
    
    void SpawnEnemy()                       // generem un enemic a lalçada actual del jugador
    {
        float rightEdge, nextSpawnTime;
        Vector3 spawnPosition;

        rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        spawnPosition = new Vector3( rightEdge + 1f, playerBird.position.y, 0f );   // prenem lalçada del jugador en aquest instant (Y);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke(nameof(SpawnEnemy), nextSpawnTime);
    }
}