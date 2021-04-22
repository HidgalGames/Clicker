using UnityEditor;
using System;

[CustomEditor(typeof(NamedEntity))]
[CanEditMultipleObjects]
public class NamedEntityEditor : Editor
{
    NamedEntity component;
    SerializedObject componentObject;
    SerializedProperty nameProperty;

    void OnEnable()
    {
        component = (NamedEntity)target;
        componentObject = new SerializedObject(component);
        nameProperty = componentObject.FindProperty("EntityName");
    }

    public override void OnInspectorGUI()
    {
        componentObject.Update();

        EditorGUILayout.LabelField("Имя:");
        StringDrawer.DrawTranslatableString(nameProperty);

        componentObject.ApplyModifiedProperties();
    }
}
