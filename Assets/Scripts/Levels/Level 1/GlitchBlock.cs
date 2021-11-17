using UnityEngine;

public class GlitchBlock : MonoBehaviour
{
    public float speed;
    public float height;

    private GameObject player;
    private BoxCollider2D trigger;

    private bool active = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        float y = player.transform.position.y - transform.position.y;
        if (y < 0)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != player || !active) return;

        float y = player.transform.position.y - transform.position.y - height;
        if (y < 0)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject != player) return;

        active = false;
    }
}
