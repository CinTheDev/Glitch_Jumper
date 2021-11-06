using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    [Header("X and Y are position, Z is scale")]
    public Vector3[] cameraInfo;
    public float speed;
    public int index;

    private Camera cam;

    public void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void FixedUpdate()
    {
        float posA = Mathf.Lerp(transform.position.x, cameraInfo[index].x, speed);
        float posB = Mathf.Lerp(transform.position.y, cameraInfo[index].y, speed);

        transform.position = new Vector3(posA, posB, transform.position.z);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cameraInfo[index].z, speed);
    }

    public void OnValidate()
    {
        index = Mathf.Clamp(index, 0, cameraInfo.Length - 1);
    }
}
