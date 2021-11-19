using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OneWayDoor))]
public class OWDInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        OneWayDoor owd = target as OneWayDoor;

        SpriteRenderer sprite = owd.gameObject.GetComponent<SpriteRenderer>();

        sprite.flipX = owd.direction == -1;
    }
}
