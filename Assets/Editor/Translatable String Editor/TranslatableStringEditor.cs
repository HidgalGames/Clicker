using UnityEditor;
using System;
using UnityEngine;

[CustomEditor(typeof(TranslatableString))]
[CanEditMultipleObjects]
public class TranslatableStringEditor : Editor
{
    TranslatableString component;
    SerializedObject componentObject;
    SerializedProperty stringProperty;

    SerializedProperty showBigTextArea;

    void OnEnable()
    {
        component = (TranslatableString)target;
        componentObject = new SerializedObject(component);
        stringProperty = componentObject.FindProperty("strings");

        showBigTextArea = componentObject.FindProperty("bigText");
    }

    public override void OnInspectorGUI()
    {
        componentObject.Update();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Большой текст", GUILayout.Width(100f));
        showBigTextArea.boolValue = EditorGUILayout.Toggle("", showBigTextArea.boolValue, GUILayout.Width(20f));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(5f);

        StringDrawer.DrawTranslatableString(stringProperty, showBigTextArea.boolValue);

        componentObject.ApplyModifiedProperties();
    }
}
