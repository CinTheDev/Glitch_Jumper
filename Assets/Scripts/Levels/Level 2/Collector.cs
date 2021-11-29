using UnityEngine;
using System.Linq;

public class Collector : MonoBehaviour
{
    public Spawner spawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawner.spawns.Contains(collision.gameObject))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
