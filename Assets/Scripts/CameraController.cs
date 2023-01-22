using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float leftLimit;
    [SerializeField]
    public float rightLimit;
    [SerializeField]
    public float bottomLimit;
    [SerializeField]
    public float upperLimit;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit), 
            transform.position.z
            );
    }
}
