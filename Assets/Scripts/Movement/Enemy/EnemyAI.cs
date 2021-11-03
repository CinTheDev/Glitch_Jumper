using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public float speed;
    public float stiffness;
    public float punchForce;
    public float punchForce2;

    private Vector3 home;
    private enum AIState
    {
        Idle,
        Follow,
        Stop,
    }

    private AIState state;

    // Start is called before the first frame update
    void Start()
    {
        state = AIState.Idle;
        home = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move;
        switch (state)
        {
            case AIState.Idle:
                move = home.x - transform.position.x;
                break;

            case AIState.Follow:
                move = player.transform.position.x - transform.position.x;
                break;

            default:
                move = 0;
                break;
        }

        if (move != 0)
        {
            // Calculate speed with function f(x)
            int direction = (int)Mathf.Sign(move);
            move = -Mathf.Abs(stiffness / (move)) + 1;
            move = Mathf.Clamp01(move);
            // The functions gives a Value from 0 to 1 based of the distance
            // Then multiply by certain constants and apply
            move *= speed * Time.fixedDeltaTime * direction;
            transform.Translate(new Vector2(move, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == AIState.Stop) return;

        // Kill player
        if (collision.gameObject == player && !player.GetComponent<PlayerMovement>().isDashing)
        {
            // Mark player as dead
            state = AIState.Stop;
            player.GetComponent<PlayerMovement>().isDead = true;

            float direction = Mathf.Sign(player.transform.position.x - transform.position.x) * punchForce;

            // Apply knockback on player
            Rigidbody2D plRb = player.GetComponent<Rigidbody2D>();
            plRb.velocity = new Vector2(direction, Mathf.Abs(direction)) * Mathf.Sqrt(plRb.gravityScale);

            // Add friction so the player doesn't slide around
            PhysicsMaterial2D mat = new PhysicsMaterial2D(plRb.sharedMaterial.name);
            mat.friction = 0.4f;
            plRb.sharedMaterial = mat;

            Debug.Log("Killed player");
        }
        // Kill enemy
        else if (collision.gameObject == player && player.GetComponent<PlayerMovement>().isDashing)
        {
            // Mark enemy as dead
            state = AIState.Stop;
            // Apply knockback on player
            Rigidbody2D plRb = player.GetComponent<Rigidbody2D>();
            plRb.velocity = new Vector2(plRb.velocity.x * -.5f, plRb.velocity.y) * Mathf.Sqrt(plRb.gravityScale);

            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * punchForce2, rb.velocity.y) * Mathf.Sqrt(rb.gravityScale);

            Debug.Log("Got hit by player");
        }
    }

    public void TriggerEnter(Collider2D collision)
    {
        if (collision.gameObject != player || state == AIState.Stop) return;
        state = AIState.Follow;
        //Debug.Log("Follow");
    }

    public void TriggerExit(Collider2D collision)
    {
        if (collision.gameObject != player || state == AIState.Stop) return;
        state = AIState.Idle;
        //Debug.Log("Idle");
    }
}
