using UnityEngine;

public class PositionCamera : MonoBehaviour
{
    [Header("X and Y are position, Z is scale")]
    public Vector3[] cameraInfo;
    public float speed;
    public int index;

    private Camera cam;

    public void Awake()
    {
        // Get Camera
        cam = GetComponent<Camera>();
    }

    public void FixedUpdate()
    {
        // Linearily interpolate x and y positions
        float posX = Mathf.Lerp(transform.position.x, cameraInfo[index].x, speed);
        float posY = Mathf.Lerp(transform.position.y, cameraInfo[index].y, speed);

        // Set new positions
        transform.position = new Vector3(posX, posY, transform.position.z);

        // Interpolate size
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cameraInfo[index].z, speed);
    }

    public void OnValidate()
    {
        // When index is changed in inspector, limit it to the size of the array
        index = Mathf.Clamp(index, 0, cameraInfo.Length - 1);
    }

    public void SetPosition()
    {
        // Set camera position and size to target immediately
        transform.position = new Vector3(cameraInfo[index].x, cameraInfo[index].y, transform.position.z);
        cam.orthographicSize = cameraInfo[index].z;
    }
}
