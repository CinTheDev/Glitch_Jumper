using UnityEngine;
using System.Linq;

public class Jumppad : ActivationClass
{
    public bool spriteInverted;
    public bool active;
    public float height;

    public GameObject[] triggerObjects;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Act()
    {
        active = true;
        animator.SetBool("Active", !spriteInverted);
    }
    protected override void Deact()
    {
        active = false;
        animator.SetBool("Active", spriteInverted);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggerObjects.Contains(collision.gameObject) || !active) return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        float vel = Mathf.Sqrt(2 * rb.gravityScale * 9.81f * height);
        rb.velocity = new Vector2(rb.velocity.x, vel);
        animator.SetTrigger("Jump");
    }
}
