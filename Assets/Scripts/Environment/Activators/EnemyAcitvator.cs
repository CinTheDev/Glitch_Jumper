using UnityEngine;

public class EnemyAcitvator : ActivationClass
{
    public EnemyAI enemy;
    protected override void Act()
    {
        enemy.active = true;
        //enemy.GetComponent<Rigidbody2D>().simulated = true;
        //enemy.GetComponent<EnemyAI>().state = EnemyAI.AIState.Idle;
    }

    protected override void Deact()
    {
        enemy.active = false;
        //enemy.GetComponent<Rigidbody2D>().simulated = false;
        //enemy.GetComponent<EnemyAI>().state = EnemyAI.AIState.Stop;
    }
}
