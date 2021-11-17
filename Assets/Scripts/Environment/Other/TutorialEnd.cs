using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    private Transform player;
    private PositionCamera cam;
    public Vector2 nextPosition;
    private GameObject respawn;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PositionCamera>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnSystem>().prefab;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            StartCoroutine(player.GetComponent<PlayerMovement>().Sleep(1/4f));
            player.position = nextPosition;
            cam.index++;
            CreateRespawn();
        }
    }

    private void CreateRespawn()
    {
        GameObject r = Instantiate(respawn, nextPosition, Quaternion.identity);
        GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnSystem>().indexend++;
        r.GetComponent<Respawn>().index = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnSystem>().indexend; 
    }
}
