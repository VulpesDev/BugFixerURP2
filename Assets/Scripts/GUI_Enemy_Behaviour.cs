using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy_Behaviour))]
public class GUI_Enemy_Behaviour : Editor
{
    Enemy_Behaviour eb;
    Enemy_Behaviour.EnemyType typeEnemy;

    public override void OnInspectorGUI()
    {

        eb = (Enemy_Behaviour)target;
        typeEnemy = eb.typeEnemy;

        switch(typeEnemy)
        {
            case Enemy_Behaviour.EnemyType.Bomber: DisplayBomberInfo();
                break;
            case Enemy_Behaviour.EnemyType.Shooter: DisplayShooterInfo();
                break;
        }
        serializedObject.ApplyModifiedProperties();

    }
    void DisplayBomberInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("typeEnemy"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("angularSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("acceleration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("health"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("keepDistanceA"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("keepDistanceB"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("explodeDamage"));
    }
    void DisplayShooterInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("typeEnemy"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("angularSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("acceleration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("health"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("firerate"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("keepDistanceA"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("keepDistanceB"));
    }
}
