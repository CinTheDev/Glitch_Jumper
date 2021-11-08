using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum DieCause
    {
        Player,
        Enemy,
        KillTrigger,
    }

    public virtual void Die(DieCause cause)
    {

    }
}
