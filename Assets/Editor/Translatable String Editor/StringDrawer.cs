using UnityEngine;
using UnityEditor;
using System;

public static class StringDrawer
{
    public static void DrawTranslatableString(SerializedProperty strings, bool showTextArea = false)
    {
        string[] langs = Enum.GetNames(typeof(Languages));

        if (strings != null)
        {
            for (int i = 0; i < langs.Length; i++)
            {
                SerializedProperty element = strings.GetArrayElementAtIndex(i);
                if (element != null)
                {
                    DrawStringElement(langs[i], element, showTextArea);
                }
                else
                {
                    strings.InsertArrayElementAtIndex(i);
                    DrawStringElement(langs[i], strings.GetArrayElementAtIndex(i), showTextArea);
                }
            }
        }
        else
        {
            for (int i = 0; i < langs.Length; i++)
            {
                strings.InsertArrayElementAtIndex(i);
                DrawStringElement(langs[i], strings.GetArrayElementAtIndex(i), showTextArea);
            }
        }
    }

    private static void DrawStringElement(string languageName, SerializedProperty stringElement, bool showTextArea = false)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(languageName, GUILayout.Width(30f));

        if (showTextArea)
        {
            stringElement.stringValue = EditorGUILayout.TextArea(stringElement.stringValue);            
        }
        else
        {
            stringElement.stringValue = EditorGUILayout.TextField(stringElement.stringValue);
        }

        EditorGUILayout.EndHorizontal();
    }
}
