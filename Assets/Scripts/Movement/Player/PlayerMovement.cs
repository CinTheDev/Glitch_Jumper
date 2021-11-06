using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed;
    public float jumpHeight;
    public float dashForce;
    public bool isActive = true;
    public bool isDashing = false;

    Vector2 move;
    bool canDash = true;
    int xdirection = 1;//-1 = left, 1 = right
    int ydirection = 1;//-1 = down, 1 = up
    
    enum Direction
    {
        xdirection,
        ydirection
    }
    Direction direction;

    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isActive)
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
            if (direction == Direction.xdirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
            
        }

        // If y velocity is 0...
        if (Mathf.Abs(rb.velocity.y) < 0.0001f && !isDashing)
        {
            // ...and jump button is pressed...
            if (Input.GetButtonDown("Jump"))
            {
                // ...jump!
                float vel = Mathf.Sqrt(2 * rb.gravityScale * 9.81f * jumpHeight);
                rb.velocity = new Vector2(rb.velocity.x, vel);
            }
            // Enable dash
            canDash = true;
        }
        // Determine direction
        if (Input.GetAxis("Horizontal") != 0)
        {
            xdirection = (int)Mathf.Sign(Input.GetAxis("Horizontal"));
            direction = Direction.xdirection;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            ydirection = (int)Mathf.Sign(Input.GetAxis("Vertical"));
            direction = Direction.ydirection;
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (isDashing == false && canDash == true)
            {
                if(direction == Direction.xdirection)
                {
                    StartCoroutine(Dash());
                    rb.velocity = new Vector2(xdirection * (dashForce/0.2f), 0);
                }
                else
                {
                    StartCoroutine(Dash());
                    rb.velocity = new Vector2(rb.velocity.x, ydirection * (dashForce/0.2f));
                }
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
    }

    public void Kill()
    {
        isActive = false;
        PhysicsMaterial2D mat = new PhysicsMaterial2D(GetComponent<Rigidbody2D>().sharedMaterial.name);
        mat.friction = 0.4f;
        GetComponent<Rigidbody2D>().sharedMaterial = mat;
    }

    public IEnumerator Sleep(float s)
    {
        isActive = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(s);
        isActive = true;
    }
}
