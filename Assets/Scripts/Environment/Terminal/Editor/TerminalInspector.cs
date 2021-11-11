using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerminalTrigger))]
public class TerminalInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var terminal = target as TerminalTrigger;
        serializedObject.Update();

        if (terminal.mode == TerminalTrigger.Mode.TypeMultiple)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("textArray"));
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("text"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
