using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public enum Mode
    {
        OutOfBounds,
        Spike,
    }
    public Mode mode;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            // Kill player
            player.GetComponent<PlayerMovement>().Kill();
        }
    }
}
