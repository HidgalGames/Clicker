using UnityEngine;
using UnityEditor;

public class DropItemEditWindow : EditorWindow
{
    public DropperEditor editor;
    public int currentItemIndex;
    public ItemToDrop item = new ItemToDrop();

    private static float standartMinHeight = 200f;
    //private static float standartMinWidth = 450f;

    private float minHeight = 200f;
    private float minWidth = 450f;

    public static DropItemEditWindow ShowWindow()
    {
        DropItemEditWindow window = (DropItemEditWindow)EditorWindow.CreateWindow<DropItemEditWindow>();
        window.titleContent = new GUIContent("Редактор предмета");

        return window;
    }

    void OnGUI()
    {
        minSize = new Vector2(minWidth, minHeight);
        maxSize = new Vector2(2048f, minHeight);

        EditorGUILayout.Space();

        DrawPreviewImage();

        item.ItemObject = (GameObject)EditorGUILayout.ObjectField("Префаб", item.ItemObject, typeof(GameObject), false);

        item.DropItemType = (ItemType)EditorGUILayout.EnumPopup("Тип предмета", item.DropItemType);

        ShowDropStats();

        ShowCancelApplyButtons();
    }

    private void DrawPreviewImage()
    {
        if (item.ItemObject)
        {
            Texture2D preview = AssetPreview.GetAssetPreview(item.ItemObject);
            if (preview)
            {
                Vector2 imageSize = new Vector2(100f, 100f);
                Rect previewPosition = new Rect(new Vector2(position.width / 2 - imageSize.x / 2, 10f), imageSize);
                EditorGUI.DrawPreviewTexture(previewPosition, preview);
                EditorGUILayout.Space(imageSize.y + 10f);

                minHeight = standartMinHeight + imageSize.y + 10f;
            }
            else
            {
                minHeight = standartMinHeight;
            }
        }
    }

    private void ShowDropStats()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField(new GUIContent("Кол-во предметов и соответствующие шансы выпадения(от 0 до 1):"));

        ShowAddRemoveButtons();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Кол-во:", GUILayout.Width(50f));
        for (int i = 0; i < item.DropCount.Count; i++)
        {
            item.DropCount[i] = EditorGUILayout.IntField(item.DropCount[i]);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Шанс:", GUILayout.Width(50f));
        for (int i = 0; i < item.DropChance.Count; i++)
        {
            item.DropChance[i] = EditorGUILayout.FloatField(item.DropChance[i]);
            item.DropChance[i] = Mathf.Clamp01(item.DropChance[i]);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
    }

    private void ShowAddRemoveButtons()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-"))
        {
            item.DropCount.RemoveAt(item.DropCount.Count - 1);
            item.DropChance.RemoveAt(item.DropChance.Count - 1);
        }

        if (GUILayout.Button("+"))
        {
            item.DropCount.Add(0);
            item.DropChance.Add(0);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void ShowCancelApplyButtons()
    {
        EditorGUILayout.Space(40);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Cancel"))
        {
            item.Clear();
            Close();
        }
        if (GUILayout.Button("Apply"))
        {
            editor.ApplyChangesToElementAtIndex(currentItemIndex, item);
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}
