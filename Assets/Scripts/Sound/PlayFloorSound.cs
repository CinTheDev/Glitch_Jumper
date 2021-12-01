using UnityEngine;

public class PlayFloorSound : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<AudioManager>().Play("FloorCollision");
    }
}
