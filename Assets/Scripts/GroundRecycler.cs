using UnityEngine;

public class GroundRecycler : MonoBehaviour
{
    [SerializeField] 
    private float overlap = 0f;
    
    private Camera mainCamera;
    private Collider2D groundCollider;
    private float groundWidth;
    private float groundBound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {    
        mainCamera  = Camera.main;                       // obtenim la camera i calculem lamplada i el limit dret del tram a partir del seu colisionador
        groundCollider = GetComponent<Collider2D>();
        groundWidth = groundCollider.bounds.size.x;
        groundBound = groundCollider.bounds.max.x;
    }

    // Update is called once per frame
    void Update()
    {
        float leftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        
        if (groundBound < leftEdge)                     // recoloquem el tram davant quan queda fora de la camera per repetir el terra o el sostre
        {
            float displacement = (groundWidth - overlap) * 2f;
            transform.position = transform.position + Vector3.right * displacement;
            groundBound = groundBound + displacement;
        }
    }
}