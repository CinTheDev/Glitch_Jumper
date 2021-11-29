using UnityEngine;
using System.Linq;

public class MovingPlatform : ActivationClass
{
    public GameObject[] transportables;
    public Vector2 Motion;
    public float timeScale;

    private Vector3 initialPos;
    private Vector3 lastPos;
    private float time;
    private bool active = true;

    protected override void Act()
    {
        active = true;
    }
    protected override void Deact()
    {
        active = false;
    }

    public void Start()
    {
        initialPos = transform.position;
    }

    public void Update()
    {
        if (!active) return;

        time += Time.deltaTime;
        float timeSin = 0.5f * Mathf.Sin(time * timeScale) + 0.5f;

        GetComponent<Rigidbody2D>().MovePosition(new Vector3(initialPos.x + timeSin * Motion.x, initialPos.y + timeSin * Motion.y));
    }

    private void LateUpdate()
    {
        lastPos = transform.position;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (transportables.Contains(collision.gameObject) && active)
        {
            float speed = transform.position.x - lastPos.x;

            if (!collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed / Time.deltaTime, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                //Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                //rb.MovePosition(new Vector2(speed, 0));
            }
            else
                collision.gameObject.transform.Translate(new Vector3(speed, 0));
        }
    }
}
