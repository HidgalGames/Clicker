using UnityEngine;
using UnityEditor;
using System;

public static class StringDrawer
{
    public static void DrawTranslatableString(SerializedProperty stringProperty)
    {
        string[] langs = Enum.GetNames(typeof(Languages));
        SerializedProperty strings = stringProperty.FindPropertyRelative("strings");
        if (strings != null)
        {
            if (strings.arraySize > 0)
            {
                for (int i = 0; i < langs.Length; i++)
                {
                    SerializedProperty element = strings.GetArrayElementAtIndex(i);
                    if (element != null)
                    {
                        DrawStringElement(langs[i], element);
                    }
                    else
                    {
                        strings.InsertArrayElementAtIndex(i);
                        DrawStringElement(langs[i], strings.GetArrayElementAtIndex(i));
                    }
                }
            }
            else
            {
                for (int i = 0; i < langs.Length; i++)
                {
                    strings.InsertArrayElementAtIndex(i);
                    DrawStringElement(langs[i], strings.GetArrayElementAtIndex(i));
                }
            }
        }
    }

    private static void DrawStringElement(string languageName, SerializedProperty stringElement)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(languageName, GUILayout.Width(30f));
        stringElement.stringValue = EditorGUILayout.TextField(stringElement.stringValue);
        EditorGUILayout.EndHorizontal();
    }
}
