using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TutorialEnd))]
public class EndEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Trigger") && Application.IsPlaying(this))
        {
            TutorialEnd o = target as TutorialEnd;
            o.NextLevel();
        }
    }
}
