using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyAI))]
class DrawLine : Editor
{
    private void OnSceneGUI()
    {
        EnemyAI enemy = target as EnemyAI;
        if (enemy == null) return;

        Handles.color = Color.green;

        Vector2 p1 = new Vector2(-enemy.relativeTriggerSize.x, enemy.relativeTriggerSize.y);
        Vector2 p2 = new Vector2(enemy.relativeTriggerSize.x, enemy.relativeTriggerSize.y);
        Vector2 p3 = new Vector2(enemy.relativeTriggerSize.x, -enemy.relativeTriggerSize.y);
        Vector2 p4 = new Vector2(-enemy.relativeTriggerSize.x, -enemy.relativeTriggerSize.y);

        Vector2 pos = enemy.transform.position;
        p1 += pos + enemy.relativeTriggerOffset;
        p2 += pos + enemy.relativeTriggerOffset;
        p3 += pos + enemy.relativeTriggerOffset;
        p4 += pos + enemy.relativeTriggerOffset;

        float thickness = 1f;

        Handles.DrawLine(p1, p2, thickness);
        Handles.DrawLine(p2, p3, thickness);
        Handles.DrawLine(p3, p4, thickness);
        Handles.DrawLine(p4, p1, thickness);
    }
}