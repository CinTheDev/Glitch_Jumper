using UnityEngine;

public class CameraOverride : MonoBehaviour
{
    public float speed;
    public GameObject terminal;

    private GameObject cam;
    private GameObject player;

    private bool active;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.GetComponent<PositionCamera>().enabled = false;
        active = true;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            float yPos = Mathf.Lerp(cam.transform.position.y, player.transform.position.y, speed);

            cam.transform.position = new Vector3(cam.transform.position.x, yPos, -10);
        }

        float terminalPos = Mathf.Lerp(terminal.transform.position.y, cam.transform.position.y, 0.5f);
        
        terminal.transform.position = new Vector3(terminal.transform.position.x, terminalPos, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cam.GetComponent<PositionCamera>().enabled = true;
        active = false;
    }
}
