using UnityEngine;

public class DashableWall : MonoBehaviour
{
    private PlayerMovement player;
    private BoxCollider2D boxCollider;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        if (player.isDashing)
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }
    }
}
