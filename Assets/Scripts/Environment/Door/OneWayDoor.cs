using UnityEngine;
using System.Linq;

public class OneWayDoor : ActivationClass
{
    public bool spriteInverted;
    [Tooltip("1 means right, -1 means left")]
    public int direction;
    public GameObject[] passables;

    private bool active = true;
    private Vector2 normal;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().flipX = direction != 1;
    }

    public void Start()
    {
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle) * direction;
        float y = Mathf.Sin(angle) * direction;
        normal = new Vector2(x, y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!passables.Contains(collision.gameObject)) return;

        // If unpassable object or not active
        if (!active)
        {
            Block();
        }

        // If player is on the wrong side
        if (Vector2.Dot(collision.transform.position - transform.position, normal) > 0)
        {
            Block();
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        // If passable leaves trigger
        if (passables.Contains(collision.gameObject)) Release();
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

    protected override void Act()
    {
        animator.SetBool("Active", !spriteInverted);
        active = true;
    }

    protected override void Deact()
    {
        animator.SetBool("Active", spriteInverted);
        active = false;
    }
}
