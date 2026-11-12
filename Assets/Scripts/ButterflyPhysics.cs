using UnityEngine;

public class ButterflyPhysics : MonoBehaviour
{
    //Movement speed 
    [SerializeField]
    float speed = 2.0f;
    
    Rigidbody2D butterflyBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        butterflyBody = GetComponent<Rigidbody2D>();
        butterflyBody.linearVelocity = Vector2.left * speed;
    }

    // Update is called once per frame
    void Update()
    {        
    }
}