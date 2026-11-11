using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float leftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        if (transform.position.x < leftEdge - 1f)
        {
            Destroy(gameObject);
        }
    }
}
