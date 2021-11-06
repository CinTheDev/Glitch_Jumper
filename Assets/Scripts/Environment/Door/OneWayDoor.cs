using UnityEngine;
using System.Linq;

public class OneWayDoor : ActivationClass
{
    public bool spriteInverted;
    [Tooltip("1 means right, -1 means left")]
    public int direction;
    public GameObject[] passables;

    private bool active = true;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().flipX = direction != 1;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // If collision is not a passable, do nothing
        if (!passables.Contains(collision.gameObject) || !active) return;

        int dir = (int)Mathf.Sign(transform.position.x - collision.transform.position.x);
        if (dir == direction)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!passables.Contains(collision.gameObject) || !active) return;

        GetComponent<BoxCollider2D>().isTrigger = false;
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
