using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerMovement : Entity
{
    public Rigidbody2D rb;
    public float movementSpeed;
    public float jumpHeight;
    public float dashForce;
    public bool isActive = true;
    public bool isDashing = false;
    [HideInInspector]
    public bool fling;

    private Vector2 move;
    private int ydirection = 1;//-1 = down, 1 = up
    private Animator animator;
    private bool canDash = true;
    private int xdirection = 1;//-1 = left, 1 = right
    private PhysicsMaterial2D mat;
    private bool flingEnabled = false;

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

    [HideInInspector]
    public enum Direction
    {
        xdirection,
        ydirection
    }
    [HideInInspector]
    public Direction direction;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        mat = new PhysicsMaterial2D(GetComponent<Rigidbody2D>().sharedMaterial.name);
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            GameObject.Find("RespawnSystem").GetComponent<RespawnSystem>().StartPlayer();
        } 
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        //Bugs
        if (deactDash == true)
        {
            canDash = false;
        }

        if (!fling)
        {
            // Movement left/right
            move = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, rb.velocity.y);
            
            switch (deactmove)
            {
                case DeactMove.not:
                    break;

                case DeactMove.both:
                    move = new Vector2(0, rb.velocity.y);
                    break;

                case DeactMove.left:
                    if (move.x < 0)
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
        mat.friction = 0.4f;
        GetComponent<Rigidbody2D>().sharedMaterial = mat;
        StartCoroutine(GameObject.Find("RespawnSystem").GetComponent<RespawnSystem>().Reload());
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
        float left = -0.499f;
        float right = 0.998f;
        float up = -0.51f;
        float down = 0.1f;

        Vector2 p1 = transform.position + new Vector3(left, up);
        Vector2 p2 = transform.position + new Vector3(left, up - down);
        Vector2 p3 = p2 + new Vector2(right, 0);
        Vector2 p4 = p1 + new Vector2(right, 0);

        bool r1 = Physics2D.Raycast(p1, Vector2.right, right);
        bool r2 = Physics2D.Raycast(p2, Vector2.right, right);
        bool r3 = Physics2D.Raycast(p1, Vector2.down, down);
        bool r4 = Physics2D.Raycast(p4, Vector2.down, down);

        Debug.DrawLine(p1, p2);
        Debug.DrawLine(p2, p3);
        Debug.DrawLine(p3, p4);
        Debug.DrawLine(p4, p1);

        return r1 || r2 || r3 || r4;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!flingEnabled && !blocks.Contains(collision.gameObject))
        {
            fling = false;
            deactDash = false;
        }
    }

    private GameObject[] blocks = new GameObject[0];
    public IEnumerator Fling(GameObject[] blocks)
    {
        deactDash = true;
        this.blocks = blocks;
        flingEnabled = true;
        for (int i = 0; i < 3; i++)
        {
            fling = true;
            yield return new WaitForEndOfFrame();
        }
        flingEnabled = false;
    }
}
