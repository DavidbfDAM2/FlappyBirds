using UnityEngine;

public class ButterflyPhysics : MonoBehaviour
{
    // Velocitat de moviment 
    [SerializeField]
    float speed;
    
    Rigidbody2D butterflyBody;

    // Inicialitzem els components abans de la primera actualització.    
    void Start()
    {
        butterflyBody = GetComponent<Rigidbody2D>();            // obtenim el cos fisic de la papallona i li donem moviment cap a lesquerra
        butterflyBody.linearVelocity = Vector2.left * speed;
    }

    // Executem aquestes comprovacions a cada fotograma.
    void Update()
    {        
    }
}