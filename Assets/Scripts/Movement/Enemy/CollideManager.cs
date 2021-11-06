using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideManager : MonoBehaviour
{
    public EnemyAI enemyAI;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        enemyAI.TriggerEnter(collision);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        enemyAI.TriggerExit(collision);
    }
}
