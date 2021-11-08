using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    public enum Mode
    {
        OutOfBounds,
        Spike,
    }
    public Mode mode;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Entity obj = collision.gameObject.GetComponent<Entity>();
        if (obj)
        {
            // Kill object
            obj.Die(Entity.DieCause.KillTrigger);
        }
    }
}
