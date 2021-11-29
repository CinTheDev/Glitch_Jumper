using UnityEngine;

public class BrokenButton : MonoBehaviour
{
    public ActivationClass door;
    public float minSpeed;

    private Rigidbody2D player;
    private bool colliding;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public void Collision(float speed)
    {
        if (!colliding) return;

        if (Mathf.Abs(speed) > minSpeed)
        {
            door.Activate();
            GetComponent<Animator>().SetBool("Active", true);
            FindObjectOfType<AudioManager>().Play("Button_Activate");
            GetComponent<TerminalTrigger>().Trigger();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject) colliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject) colliding = false;
    }
}
