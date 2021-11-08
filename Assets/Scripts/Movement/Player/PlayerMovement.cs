using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Entity
{
    public Rigidbody2D rb;
    public float movementSpeed;
    public float jumpHeight;
    public float dashForce;
    public bool isActive = true;
    public bool isDashing = false;
    public bool respawn = false;

    Vector2 move;
    bool canDash = true;
    int xdirection = 1;//-1 = left, 1 = right
    int ydirection = 1;//-1 = down, 1 = up
    [HideInInspector]
    public Animator animator;

    [Header("Deactivate Mechanics")]
    public bool deactJump = false;
    public bool deactDash = false;
    public enum DeactMove
    {
        not,
        both,
        left,
        right,
    }
    public DeactMove deactmove;

    
    enum Direction
    {
        xdirection,
        ydirection
    }
    Direction direction;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        transform.position = GetComponent<RespawnPlayer>().respawnpoint;
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
        move = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, rb.velocity.y);
        //Bugs
        if (deactDash == true)
        {
            canDash = false;
        }
        switch (deactmove)
        {
            case DeactMove.not:
                break;

            case DeactMove.both:
                move = new Vector2(0,rb.velocity.y);
                break;

            case DeactMove.left:
                if(move.x < 0)
                {
                    move = new Vector2(0, rb.velocity.y);
                }
                else
                {
                    //NOTHING
                }
                break;
            case DeactMove.right:
                if (move.x > 0)
                {
                    move = new Vector2(0, rb.velocity.y);
                }
                else
                {
                    //NOTHING
                }
                break;
        }
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

        //if the collider of the second object detects an object
        if (isGrounded() && !isDashing)
        {
            // ...and jump button is pressed...
            if (Input.GetButtonDown("Jump") && deactJump== false)
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
            animator.SetInteger("direction", xdirection);
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
        animator.SetBool("isDashing", true);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(0, 0.0002f);
        isDashing = false;
        animator.SetBool("isDashing", false);
        canDash = false;
    }

    public override void Die(DieCause cause)
    {
        isActive = false;
        animator.SetBool("Death", true);
        PhysicsMaterial2D mat = new PhysicsMaterial2D(GetComponent<Rigidbody2D>().sharedMaterial.name);
        mat.friction = 0.4f;
        GetComponent<Rigidbody2D>().sharedMaterial = mat;
        respawn = true;
    }

    public IEnumerator Sleep(float s)
    {
        isActive = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(s);
        isActive = true;
    }

    private bool isGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }
}
