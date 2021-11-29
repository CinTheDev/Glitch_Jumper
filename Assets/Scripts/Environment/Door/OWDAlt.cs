using UnityEngine;

public class OWDAlt : MonoBehaviour
{
    // This is to exploit a certain glitch, the original script was not spaghetti enough to have that glitch

    [Tooltip("1 means right, -1 means left")]
    public int direction;
    public GameObject player;

    private Vector2 normal;

    public void Awake()
    {
        GetComponent<SpriteRenderer>().flipX = direction != 1;

        Block();
    }

    public void Start()
    {
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle) * direction;
        float y = Mathf.Sin(angle) * direction;
        normal = new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != player) return;

        if (Vector2.Dot(collision.transform.position - transform.position, normal) < 0)
        {
            Release();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        Block();
    }


    private void Block()
    {
        // Make wall solid
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
    private void Release()
    {
        // Make wall passable
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void OnValidate()
    {
        // Direction can only be 1 or -1
        direction = (int)Mathf.Sign(direction);
    }
}
