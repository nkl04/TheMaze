using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SwitchBehaviour))] 
public class SwitchBehaviourEditor : Editor
{
    SerializedProperty canOpenSwitch;
    SerializedProperty canCloseSwitch;
    SerializedProperty mode;
    SerializedProperty moveableGameObject;
    SerializedProperty remainButton;

    private void OnEnable()
    {
        canOpenSwitch = serializedObject.FindProperty("canOpenSwitch");
        canCloseSwitch = serializedObject.FindProperty("canCloseSwitch");
        mode = serializedObject.FindProperty("mode");
        moveableGameObject = serializedObject.FindProperty("moveableGameObject");
        remainButton = serializedObject.FindProperty("remainButton");
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        EditorGUILayout.PropertyField(canOpenSwitch);
        EditorGUILayout.PropertyField(canCloseSwitch);
        EditorGUILayout.PropertyField(mode);
        EditorGUILayout.PropertyField(remainButton);
        EditorGUILayout.PropertyField(moveableGameObject);

        serializedObject.ApplyModifiedProperties();
    }
}
