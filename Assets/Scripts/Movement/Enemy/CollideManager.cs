using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideManager : MonoBehaviour
{
    public EnemyAI enemyAI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyAI.TriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyAI.TriggerExit(collision);
    }
}
