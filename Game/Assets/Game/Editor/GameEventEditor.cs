using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameEvent my_event = (GameEvent)target;        
        if (GUILayout.Button("Raise Event"))
        {
            my_event.Raise();
        }
    }
}
