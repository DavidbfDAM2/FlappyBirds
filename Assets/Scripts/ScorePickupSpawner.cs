using UnityEngine;

public class ScorePickupSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject scorePickupPrefab;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float spawnTime = 2f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnScorePickup", 2f, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnScorePickup()
    {
        float minY = mainCamera.transform.position.y - mainCamera.orthographicSize + 1.5f;
        float maxY = mainCamera.transform.position.y + mainCamera.orthographicSize - 1.5f;
        float randomY = Random.Range(minY, maxY);

        float rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        Instantiate(scorePickupPrefab, spawnPosition, Quaternion.identity);
    }
}
