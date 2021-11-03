using UnityEngine;
using System.Linq;

public class Jumppad : ActivationClass
{
    public bool active;
    public float jumpForce;

    public GameObject[] triggerObjects;

    public Color debugActiveColor;
    public Color debugUnactiveColor;

    protected override void Act()
    {
        active = true;
        GetComponent<SpriteRenderer>().color = debugActiveColor;
    }
    protected override void Deact()
    {
        active = false;
        GetComponent<SpriteRenderer>().color = debugUnactiveColor;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!triggerObjects.Contains(collision.gameObject) || !active) return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
