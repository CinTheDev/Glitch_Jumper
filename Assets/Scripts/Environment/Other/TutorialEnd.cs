using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    private Transform player;
    private TutorialCamera cam;
    public Vector2 nextPosition;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialCamera>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.position = nextPosition;
            cam.index++;
        }
    }
}
