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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Resum: programa el primer enemic al cap de dos segons i mig de joc.
    void Start()
    {
        Invoke(nameof(SpawnEnemy), 2.5f);
    }

    // Update is called once per frame
    // Resum: mètode buit, sense cap acció per fotograma.
    void Update()
    {

    }
    
    void SpawnEnemy()
    {
        float minY = mainCamera.transform.position.y - mainCamera.orthographicSize + 1.5f;          // generem un enemic davant de la camera a una altura aleatoria i sortegem lespera fins al seguent
        float maxY = mainCamera.transform.position.y + mainCamera.orthographicSize - 1.5f;
        float randomY = Random.Range(minY, maxY);

        float rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke(nameof(SpawnEnemy), nextSpawnTime);
    }
}