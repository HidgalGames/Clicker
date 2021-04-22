using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class TranslatableStringEditorWindow : EditorWindow
{
    private const float standartMinWidth = 400f;
    private const float standartMinHeight = 500f;

    Vector2 scrollPos;

    List<bool> showBigTextArea = new List<bool>();

    [MenuItem("Localization/String Editor")]
    public static TranslatableStringEditorWindow ShowWindow()
    {
        TranslatableStringEditorWindow window = (TranslatableStringEditorWindow)EditorWindow.GetWindow(typeof(TranslatableStringEditorWindow));
        window.titleContent = new GUIContent("Редактор Локализации");

        window.minSize = new Vector2(standartMinWidth, standartMinHeight);

        

        return window;
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("**Checkbox возле названия включает большое поле для текста**");

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        Object[] strings = AssetSearch.GetAllInstances<TranslatableString>();
        if (strings != null)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                SerializedObject obj = new SerializedObject(strings[i]);
                SerializedProperty element = obj.FindProperty("strings");
                SerializedProperty bigText = obj.FindProperty("bigText");

                EditorGUILayout.Space(5f);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(strings[i].name, EditorStyles.boldLabel, GUILayout.Width(150f));

                bigText.boolValue = EditorGUILayout.Toggle("", bigText.boolValue, GUILayout.Width(20f));
                EditorGUILayout.EndHorizontal();

                StringDrawer.DrawTranslatableString(element, bigText.boolValue);

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                obj.ApplyModifiedProperties();
            }
        }

        EditorGUILayout.EndScrollView();
    }
}
