using UnityEditor;
using System;
using UnityEngine;

[CustomEditor(typeof(NamedEntity))]
[CanEditMultipleObjects]
public class NamedEntityEditor : Editor
{
    NamedEntity component;
    SerializedObject componentObject;
    SerializedProperty strings;

    SerializedProperty nameProperty;
    SerializedProperty isUnknownProperty;

    void OnEnable()
    {
        component = (NamedEntity)target;
        componentObject = new SerializedObject(component);

        nameProperty = componentObject.FindProperty("EntityName");
        strings = nameProperty.serializedObject.FindProperty("strings");

        isUnknownProperty = componentObject.FindProperty("IsUnknown");

    }

    public override void OnInspectorGUI()
    {
        componentObject.Update();

        EditorGUILayout.PropertyField(isUnknownProperty);
        EditorGUILayout.Space(5f);

        EditorGUILayout.PropertyField(nameProperty);

        DrawStringFields();

        componentObject.ApplyModifiedProperties();
    }

    private void DrawStringFields()
    {
        if (component.EntityName != null)
        {
            SerializedObject nameObject = new SerializedObject(component.EntityName);
            strings = nameObject.FindProperty("strings");

            EditorGUILayout.Space(5f);

            EditorGUILayout.LabelField("Имя существа:");

            StringDrawer.DrawTranslatableString(strings);
            nameObject.ApplyModifiedProperties();
        }
    }
}
