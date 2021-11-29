using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Update") && Application.IsPlaying(this))
        {
            AudioManager a = target as AudioManager;
            a.UpdateSound();
        }
    }
}
