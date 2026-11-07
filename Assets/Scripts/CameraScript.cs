using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Transform targetBird;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(targetBird.position.x, transform.position.y, transform.position.z);        
    }
}
