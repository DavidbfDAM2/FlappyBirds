using UnityEngine;

public class GroundRecycler : MonoBehaviour
{
    [SerializeField] 
    private float overlap;
    
    private Camera mainCamera;
    private Collider2D groundCollider;
    private float groundWidth;
    private float groundBound;

    // Inicialitzem els components abans de la primera actualització.
    void Start()
    {    
        mainCamera  = Camera.main;                       // obtenim la camera i calculem lamplada i el limit dret del tram a partir del seu colisionador
        groundCollider = GetComponent<Collider2D>();
        groundWidth = groundCollider.bounds.size.x;
        groundBound = groundCollider.bounds.max.x;
    }

    // Executem aquestes comprovacions a cada fotograma.
    void Update()
    {
        float leftEdge, displacement;

        leftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        
        if (groundBound < leftEdge)                     // recoloquem el tram davant quan queda fora de la camera per repetir el terra o el sostre
        {                                               // pressuposem dos trams de la mateixa amplada i respectem el solapament en mourels
            displacement = (groundWidth - overlap) * 2f;
            transform.position = transform.position + Vector3.right * displacement;
            groundBound = groundBound + displacement;
        }
    }
}