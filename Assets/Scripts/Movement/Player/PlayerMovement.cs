using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed = 3;
    public float jumpForce = 7;
    public float dashForce = 10;
    public bool isDead = false;
    public bool isDashing = false;

    Vector2 move;
    float y;
    bool canDash = true;
    int direction = 1; //-1 = left, 1 = right
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        // Movement left/right
        move = new Vector2(Input.GetAxisRaw("Horizontal")* movementSpeed, rb.velocity.y);
        // Deactivate movement while dashing
        if (!isDashing)
        {
            rb.velocity = move;
        }
        // Lock y position while dashing
        else
        {
            //transform.position = new Vector2(transform.position.x, y);
            rb.velocity = new Vector2(rb.velocity.x , y);
        }

        // If y velocity is 0...
        if (Mathf.Abs(rb.velocity.y) < 0.0001f)
        {
            // ...and jump button is pressed...
            if (Input.GetButton("Jump"))
            {
                // ...jump!
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            // Enable dash
            canDash = true;
        }
        // Determine direction
        if (Input.GetAxis("Horizontal") != 0)
        {
            direction = (int)Mathf.Sign(Input.GetAxis("Horizontal"));
        }
        
        // Dash
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (isDashing == false && canDash == true)
            {
                y = transform.position.y;
                StartCoroutine(Dash());
                rb.velocity = new Vector2(direction * dashForce, 0);
            } 
        }
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(0, 0.0002f);
        isDashing = false;
        canDash = false;
        //StartCoroutine(Cooldown());
    }

    // I don't like cooldown
    /*private IEnumerator Cooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(1.5f);
        canDash = true;
    }*/
}
