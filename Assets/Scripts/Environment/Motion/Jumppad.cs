using UnityEngine;
using System.Linq;
using System.Collections;

public class Jumppad : ActivationClass
{
    public bool spriteInverted;
    public bool active;
    public Vector2 height;

    public GameObject[] triggerObjects;

    public GameObject[] flingBlock;

    private Animator animator;
    private bool isFalling = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Active", spriteInverted != active);
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

    private void Jump(GameObject obj)
    {
        if (isFalling && !triggerObjects.Contains(obj))
        {
            StartCoroutine(Land());
        }

        if (!triggerObjects.Contains(obj) || !active) return;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        float velY = Mathf.Sqrt(2 * rb.gravityScale * 9.81f * height.y);
        rb.velocity = new Vector2(height.x, velY);
        animator.SetTrigger("Jump");

        if (obj.GetComponent<PlayerMovement>() && height.x != 0)
        {
            StartCoroutine(obj.GetComponent<PlayerMovement>().Fling(flingBlock));
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Jump(collision.gameObject);
    }

    private IEnumerator Land()
    {
        Deact();
        isFalling = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void Fall()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        isFalling = true;
    }
}
