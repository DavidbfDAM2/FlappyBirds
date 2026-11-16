using UnityEngine;

public class ScorePickupSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject scorePickupPrefab;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float minSpawnTime;

    [SerializeField]
    
    float maxSpawnTime;

    // Inicialitzem els components abans de la primera actualització.    
    void Start()
    {
        Invoke(nameof(SpawnScorePickup), 2f);// invoquem la primera lluerna (pickup) al cap de dos segons de joc
    }

    // Executem aquestes comprovacions a cada fotograma.
    void Update()
    {        
    }
    
    void SpawnScorePickup()
    {
        float minY, maxY, randomY, rightEdge, nextSpawnTime;
        Vector3 spawnPosition;

        minY = mainCamera.transform.position.y - mainCamera.orthographicSize + 1.5f;      // generem una lluerna (pickup) davant de la camera a una altura aleatoria i sortegem lespera fins al seguent
        maxY = mainCamera.transform.position.y + mainCamera.orthographicSize - 1.5f;
        randomY = Random.Range(minY, maxY);

        rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        Instantiate(scorePickupPrefab, spawnPosition, Quaternion.identity);

        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke(nameof(SpawnScorePickup), nextSpawnTime);
    }
}