using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dropper))]
[CanEditMultipleObjects]
public class DropperEditor : Editor
{
    Dropper component;
    SerializedObject componentObject;
    SerializedProperty itemsList;
    int listSize;

    private static GUILayoutOption miniButtonWidth = GUILayout.Width(30f);

    void OnEnable()
    {
        component = (Dropper)target;
        componentObject = new SerializedObject(component);
        itemsList = componentObject.FindProperty("ItemsToDrop");
    }

    public override void OnInspectorGUI()
    {
        componentObject.Update();

        EditorGUILayout.Space();
        component.dropTriggerType = (DropTrigger)EditorGUILayout.EnumPopup("Drop Trigger", component.dropTriggerType);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(itemsList, false);

        if (itemsList.isExpanded)
        {
            EditorGUI.indentLevel += 1;

            ShowItemsCountField();
            ShowList();

            EditorGUI.indentLevel -= 1;
        }

        componentObject.ApplyModifiedProperties();
    }

    private void ShowItemsCountField()
    {
        listSize = itemsList.arraySize;

        EditorGUILayout.BeginHorizontal();
        ShowAddRemoveButtons();
        listSize = EditorGUILayout.IntField("Items Count", listSize);
        EditorGUILayout.EndHorizontal();

        if (listSize != itemsList.arraySize)
        {
            while (listSize > itemsList.arraySize)
            {
                component.ItemsToDrop.Insert(itemsList.arraySize, new ItemToDrop());
            }
            while (listSize < itemsList.arraySize)
            {
                component.ItemsToDrop.RemoveAt(itemsList.arraySize - 1);
            }
        }
    }

    private void ShowList()
    {
        for(int i = 0; i < itemsList.arraySize; i++)
        {
            ShowItem(itemsList.GetArrayElementAtIndex(i), i);
        }
    }

    private void ShowItem(SerializedProperty item, int itemNumber)
    {
        EditorGUILayout.BeginHorizontal();
        ShowItemButtons(itemNumber);
        SerializedProperty itemObject = item.FindPropertyRelative("ItemObject");
        itemObject.objectReferenceValue = EditorGUILayout.ObjectField("Drop Item" + itemNumber, itemObject.objectReferenceValue, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();
    }

    private void ShowItemButtons(int itemNumber)
    {
        if (GUILayout.Button("Edit", EditorStyles.miniButtonLeft, miniButtonWidth))
        {
            DropItemEditWindow window = DropItemEditWindow.ShowWindow();
            window.currentItemIndex = itemNumber;
            window.editor = this;
            window.item.CopyFrom(component.ItemsToDrop[itemNumber]);
        }

        if (GUILayout.Button("+", EditorStyles.miniButtonMid, miniButtonWidth))
        {
            component.ItemsToDrop.Insert(itemNumber + 1, new ItemToDrop());
            listSize = itemsList.arraySize;
        }

        if (GUILayout.Button("-", EditorStyles.miniButtonRight, miniButtonWidth))
        {
            if (itemsList.arraySize > 0)
            {
                component.ItemsToDrop.RemoveAt(itemNumber);
                listSize = itemsList.arraySize;
            }
        }
    }

    private void ShowAddRemoveButtons()
    {
        if (GUILayout.Button("+", EditorStyles.miniButtonLeft, miniButtonWidth))
        {
            component.ItemsToDrop.Add(new ItemToDrop());
            //itemsList.InsertArrayElementAtIndex(itemsList.arraySize);
            listSize = itemsList.arraySize;
        }

        if (GUILayout.Button("-", EditorStyles.miniButtonRight, miniButtonWidth))
        {
            if (itemsList.arraySize > 0)
            {
                itemsList.DeleteArrayElementAtIndex(itemsList.arraySize - 1);
                listSize = itemsList.arraySize;
            }
        }
    }

    public void ApplyChangesToElementAtIndex(int index, ItemToDrop item)
    {
        component.ItemsToDrop[index] = item;
    }
}
