using UnityEngine;

public class EnemyAI : Entity
{
    public Vector2 relativeTriggerSize;
    public Vector2 relativeTriggerOffset;
    public float speed;
    public float stiffness;
    public float punchForce;
    public float punchForce2;
    [HideInInspector]
    public Animator animator;
    public enum Mode
    {
        Stationary,
        Walk,
    }
    [Tooltip("Stationary makes the enemy stationary and follow the player in the trigger,\n" +
            "Walk makes the enemy walk back and forth in the trigger.")]
    public Mode mode;

    private Rigidbody2D rb;
    private GameObject player;
    private Vector3 home;
    [HideInInspector]
    public enum AIState
    {
        Idle,
        Follow,
        Stop,
    }
    [HideInInspector]
    public AIState state;

    // Unity's Mathf.Sign() returns 1 when input is 0, that is not how I want it.
    private int RealSign(float val)
    {
        if (Mathf.Abs(val) < 0.2) return 0;
        return (int)Mathf.Sign(val);
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        state = AIState.Idle;
        home = transform.position;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (state == AIState.Stop) return;

        switch (mode)
        {
            case Mode.Stationary:
                Stationary();
                break;

            case Mode.Walk:
                Walk();
                break;
        }
    }

    private void Stationary()
    {
        if (Mathf.Abs(player.transform.position.x - home.x - relativeTriggerOffset.x) < relativeTriggerSize.x &&
        Mathf.Abs(player.transform.position.y - home.y - relativeTriggerOffset.y) < relativeTriggerSize.y)
        {
            state = AIState.Follow;
        }
        else
        {
            state = AIState.Idle;
        }

        float move = state switch
        {
            AIState.Idle => home.x - transform.position.x,
            AIState.Follow => player.transform.position.x - transform.position.x,
            _ => 0,
        };

        animator.SetInteger("Walk", RealSign(move));

        // Calculate speed with function f(x)
        int direction = (int)Mathf.Sign(move);
        move = -Mathf.Abs(stiffness / (move)) + 1;
        move = Mathf.Clamp01(move);
        // The functions gives a Value from 0 to 1 based of the distance
        // Then multiply by certain constants and apply
        move *= speed * Time.fixedDeltaTime * direction;
        transform.Translate(new Vector2(move, 0));        
    }

    public int direction = 1;
    private void Walk()
    {
        if (transform.position.x - home.x - relativeTriggerOffset.x > relativeTriggerSize.x)
        {
            direction = -1;
        }
        else if (transform.position.x - home.x - relativeTriggerOffset.x < -relativeTriggerSize.x)
        {
            direction = 1;
        }

        transform.Translate(new Vector3(direction * speed * Time.fixedDeltaTime, 0));
        animator.SetInteger("Walk", direction);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Kill player
        if (collision.gameObject == player && !player.GetComponent<PlayerMovement>().isDashing)
        {
            if (state == AIState.Stop) return;

            // Mark player as dead
            state = AIState.Stop;
            player.GetComponent<PlayerMovement>().Die(DieCause.Enemy);

            float direction = Mathf.Sign(player.transform.position.x - transform.position.x) * punchForce;

            // Apply knockback on player
            Rigidbody2D plRb = player.GetComponent<Rigidbody2D>();
            plRb.velocity = new Vector2(direction, Mathf.Abs(direction)) * Mathf.Sqrt(plRb.gravityScale);

            animator.SetBool("Attack", true);

            //Debug.Log("Killed player");
        }
        // Kill enemy
        else if (collision.gameObject == player && player.GetComponent<PlayerMovement>().isDashing)
        {
            Die(DieCause.Player);
        }
    }

    public override void Die(DieCause cause)
    {
        if (state == AIState.Stop) return;

        // Mark enemy as dead
        state = AIState.Stop;
        
        if (cause == DieCause.Player)
        {
            // Apply knockback on player
            Rigidbody2D plRb = player.GetComponent<Rigidbody2D>();
            plRb.velocity = new Vector2(plRb.velocity.x * -.5f, plRb.velocity.y) * Mathf.Sqrt(plRb.gravityScale);

            // Apply knockback on Enemy
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * punchForce2, rb.velocity.y);
        }

        animator.SetBool("Dead", true);

        //Debug.Log("Got hit by player");
    }

    private void OnValidate()
    {
        direction = (int)Mathf.Sign(direction);
    }
}
