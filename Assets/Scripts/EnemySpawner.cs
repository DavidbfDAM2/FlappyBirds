using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float spawnTime = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        float rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, mainCamera.transform.position.y, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
