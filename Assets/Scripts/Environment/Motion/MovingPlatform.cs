using UnityEngine;
using System.Linq;

public class MovingPlatform : MonoBehaviour
{
    public GameObject[] transportables;
    public Vector2 Motion;
    public float timeScale;

    private Vector3 initialPos;
    private Vector3 lastPos;
    private float time;

    public void Start()
    {
        initialPos = transform.position;
    }

    public void Update()
    {
        time += Time.deltaTime;
        float timeSin = 0.5f * Mathf.Sin(time * timeScale) + 0.5f;

        GetComponent<Rigidbody2D>().MovePosition(new Vector3(initialPos.x + timeSin * Motion.x, initialPos.y + timeSin * Motion.y));

        lastPos = transform.position;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (transportables.Contains(collision.gameObject))
        {
            float speed = transform.position.x - lastPos.x;
            collision.gameObject.transform.Translate(new Vector3(speed, 0));
        }
    }
}
