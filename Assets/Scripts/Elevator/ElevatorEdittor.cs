using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Elevator))] 
public class ElevatorEdittor : Editor {
    SerializedProperty light2d;
    SerializedProperty speedProp;
    SerializedProperty upperPosProp;
    SerializedProperty downPosProp;
    SerializedProperty leftPosProp;
    SerializedProperty rightPosProp;
    SerializedProperty directionProp;

    private void OnEnable()
    {
        light2d = serializedObject.FindProperty("light2d");
        speedProp = serializedObject.FindProperty("speed");
        upperPosProp = serializedObject.FindProperty("upperPos");
        downPosProp = serializedObject.FindProperty("downPos");
        leftPosProp = serializedObject.FindProperty("leftPos");
        rightPosProp = serializedObject.FindProperty("rightPos");
        directionProp = serializedObject.FindProperty("direction");
    }

    public override void OnInspectorGUI()
    {
        // Cập nhật giá trị của các SerializedProperty
        serializedObject.Update();

        EditorGUILayout.PropertyField(light2d);
        EditorGUILayout.PropertyField(speedProp);
        EditorGUILayout.PropertyField(directionProp);

        Direction selectedDirection = (Direction)directionProp.enumValueIndex;
        if (selectedDirection == Direction.Horizontal)
        {
            EditorGUILayout.PropertyField(leftPosProp);
            EditorGUILayout.PropertyField(rightPosProp);
        }
        else if (selectedDirection == Direction.Vertical)
        {
            EditorGUILayout.PropertyField(upperPosProp);
            EditorGUILayout.PropertyField(downPosProp);
        }

        // Áp dụng các thay đổi
        serializedObject.ApplyModifiedProperties();
    }
}


