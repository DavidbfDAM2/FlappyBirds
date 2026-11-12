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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        birdPhysics = targetBird.GetComponent<BirdPhysics>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float targetOffset = -birdPhysics.HorizontalInput * maxOffset;

        currentOffset = Mathf.SmoothDamp(currentOffset, targetOffset, ref offsetVelocity, smoothTime);

        transform.position = new Vector3(targetBird.position.x + currentOffset, transform.position.y, transform.position.z);
    }
}
