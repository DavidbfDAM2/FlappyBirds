using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject obstaclePrefab;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float minSpawnTime;

    [SerializeField]
    float maxSpawnTime;

    [SerializeField]
    float minHeightSpawn;

    [SerializeField]
    float maxHeightSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SpawnObstacle), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle() 
    {
        float randomY = Random.Range(minHeightSpawn, maxHeightSpawn); 
        
        float rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        
        float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke(nameof(SpawnObstacle), nextSpawnTime);
    }
}
