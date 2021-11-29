using UnityEngine;

public class LastLevelAbility : MonoBehaviour
{
    public BrokenButton button;

    private PlayerMovement player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player.gameObject) return;

        if (!player.GetComponent<PlayerBrokenButton>())
        {
            PlayerBrokenButton b = player.gameObject.AddComponent<PlayerBrokenButton>();
            b.button = button;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.canDash = true;
    }
}
