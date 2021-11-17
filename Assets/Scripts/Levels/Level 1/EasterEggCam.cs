using UnityEngine;

public class EasterEggCam : MonoBehaviour
{
    private PositionCamera cam;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PositionCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.index = 4;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cam.index = 3;
    }
}
