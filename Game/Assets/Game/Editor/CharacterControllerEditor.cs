﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterController))]
public class CharacterControllerEditor : Editor
{

    //Sats properties
    private SerializedProperty stats_base_property;
    private SerializedProperty stats_variable_property;

    //Controllers
    private SerializedProperty skill_controller_property;
    private SerializedProperty move_controller_property;
    private SerializedProperty inventory_property;

    //Camera 
    private SerializedProperty camera_property;

    private void OnEnable()
    {
        stats_base_property = serializedObject.FindProperty("base_stats");
        stats_variable_property = serializedObject.FindProperty("variables_stats");
        skill_controller_property = serializedObject.FindProperty("skill_controller");
        move_controller_property = serializedObject.FindProperty("move_controller");
        inventory_property = serializedObject.FindProperty("inventory");
        camera_property = serializedObject.FindProperty("pc_camera");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Player Stats", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Base");
        EditorGUILayout.LabelField("Variable");
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < (int)StatId._numId; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(stats_base_property.GetArrayElementAtIndex(i),new GUIContent(StatToString((StatId)i)));
            EditorGUILayout.PropertyField(stats_variable_property.GetArrayElementAtIndex(i), GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        { 
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Controllers", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(skill_controller_property);
            EditorGUILayout.PropertyField(move_controller_property);
            EditorGUILayout.PropertyField(inventory_property);
        }
        EditorGUILayout.ObjectField(camera_property);
        serializedObject.ApplyModifiedProperties();
    }


    string StatToString(StatId id)
    {
        string ret;
        switch (id)
        {
            case StatId.WeaponDmg:
                ret = "Weapon Damage";
                break;
            case StatId.MaxHealth:
                ret = "Max Health";
                break;
            case StatId.HealthRegen:
                ret = "Health Regen";
                break;
            case StatId.MaxResource:
                ret = "Max Resource";
                break;
            case StatId.ResourceRegen:
                ret = "Resource Regen";
                break;
            case StatId.MoveSpeed:
                ret = "Move Speed";
                break;
            case StatId.AttackSpeed:
                ret = "Attack Speed";
                break;
            case StatId.Armor:
                ret = "Armor";
                break;
            case StatId.PhysicRes:
                ret = "Physical Resist";
                break;
            case StatId.FireRes:
                ret = "Fire Resist";
                break;
            case StatId.WaterRes:
                ret = "Water Resist";
                break;
            case StatId.ShockRes:
                ret = "Shock Resist";
                break;
            case StatId.EarthRes:
                ret = "Earth Resistance";
                break;
            default:
                ret = "Unknown";
                break;
        }

        return ret;
    }



}
