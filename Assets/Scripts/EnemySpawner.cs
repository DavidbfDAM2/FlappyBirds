using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float minSpawnTime = 3f;

    [SerializeField]
    float maxSpawnTime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SpawnEnemy), 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        float minY = mainCamera.transform.position.y - mainCamera.orthographicSize + 1.5f;
        float maxY = mainCamera.transform.position.y + mainCamera.orthographicSize - 1.5f;
        float randomY = Random.Range(minY, maxY);

        float rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke(nameof(SpawnEnemy), nextSpawnTime);
    }
}
