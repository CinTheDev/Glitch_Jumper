using UnityEngine;

public class EasterEggCam : MonoBehaviour
{
    private PositionCamera cam;
    private GameObject player;

    public int roomIndex;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PositionCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        cam.index = roomIndex + 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        cam.index = roomIndex;
    }
}
