using UnityEngine;

public class FancyCamera : MonoBehaviour
{
    public Transform target;
    public float speed;

    public void FixedUpdate()
    {
        float a = Mathf.Lerp(transform.position.x, target.position.x, speed);
        float b = Mathf.Lerp(transform.position.y, target.position.y, speed);
        transform.position = new Vector3(a, b, transform.position.z);
    }
}
