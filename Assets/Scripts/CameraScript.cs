using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Transform targetBird;
    
    [SerializeField]
    float maxOffset;

    [SerializeField]
    float smoothTime;

    BirdPhysics birdPhysics;
    float currentOffset;
    float offsetVelocity;

    // Inicialitzem els components abans de la primera actualització.
    void Start()
    {
        birdPhysics = targetBird.GetComponent<BirdPhysics>();   // obtenim el controlador i enquadrem locell abans de començar la partida
        transform.position = new Vector3(targetBird.position.x + currentOffset, transform.position.y, transform.position.z);
    }

    // actualitzem la camera despres dels Update() dels altres objectes
    void LateUpdate()
    {
        float targetOffset;

        if (Time.timeScale > 0f && Time.deltaTime > 0f)                 // nomes actualitzem la camera quan el joc i el temps estan avançant
        {
            targetOffset = -birdPhysics.HorizontalInput * maxOffset;

            currentOffset = Mathf.SmoothDamp(currentOffset, targetOffset, ref offsetVelocity, smoothTime);                      // seguim locell horitzontalment amb un desplaçament suau segons acceleracio/frenada

            transform.position = new Vector3(targetBird.position.x + currentOffset, transform.position.y, transform.position.z);
        }
    }
}