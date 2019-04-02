using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(CharacterController))]
//public class CharacterControllerEditor : Editor
//{

//    private SerializedProperty stats_property;
//    private const string stats_property_string = "base_stats";

//    private void OnEnable()
//    {
//        stats_property = serializedObject.FindProperty(stats_property_string);
   
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        for (int i = 0; i < 11; i++)
//        {
//            EditorGUILayout.PropertyField(stats_property.GetArrayElementAtIndex(i));
//        }
//        serializedObject.ApplyModifiedProperties();
//    }

//}
