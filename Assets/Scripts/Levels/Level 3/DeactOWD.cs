using UnityEngine;

public class DeactOWD : MonoBehaviour
{
    public GameObject owd;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        owd.GetComponent<BoxCollider2D>().isTrigger = true;
        owd.GetComponent<OneWayDoor>().enabled = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        owd.GetComponent<OneWayDoor>().enabled = true;
    }
}
