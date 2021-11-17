using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Jumppad))]
public class JumppadInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Jumppad pad = target as Jumppad;

        if (GUILayout.Button("Fall") && Application.IsPlaying(this))
        {
            pad.Fall();
        }
    }
}
