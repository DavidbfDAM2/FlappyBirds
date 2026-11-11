using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject butterflyPrefab;
    
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float spawnTime = 4f;

    [SerializeField]
    BirdPhysics playerBird;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnButterfly", 2, spawnTime);      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnButterfly() 
    {
        if (playerBird.NeedsHealing()) 
        {
            float rightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
            Vector3 spawnPosition = new Vector3(rightEdge + 1f, mainCamera.transform.position.y, 0f);
            Instantiate(butterflyPrefab, spawnPosition, Quaternion.identity);
        }        
    }
}
