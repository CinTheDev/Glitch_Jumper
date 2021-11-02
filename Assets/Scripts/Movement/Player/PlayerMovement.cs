using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 move;
    public float movementSpeed = 3;
    public float jumpForce = 5;
    public float dashForce = 10;
    public bool isDead = false;
    public bool isDashing = false;
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
        //movement left/right
        move = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime * movementSpeed;
        //deactivates movement while dashing
        if (!isDashing)
        {
            transform.Translate(move);
        }
        //locks the y position while dashing
        else
        {
            transform.position = new Vector2(transform.position.x, y);
        }
        //if jump-button pressed and y velocity is 0 jump
        if (Input.GetButton("Jump") && Mathf.Abs(rb.velocity.y) < 0.0001f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        //determine direction
        if (Input.GetAxisRaw("Horizontal") > 0) 
        {
            direction = 1;
        }
        if (Input.GetAxisRaw("Horizontal") <  0)
        {
            direction = -1;
        }
        //dash
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (isDashing == false && canDash == true)
            {
                y = transform.position.y;
                StartCoroutine(DashTimer());
                rb.velocity = new Vector2(direction * dashForce, 0);
            } 
        }
    }
    private IEnumerator DashTimer()
    {
        isDashing = true;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(0, 0.0002f);
        isDashing = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(1.5f);
        canDash = true;
    }
}
