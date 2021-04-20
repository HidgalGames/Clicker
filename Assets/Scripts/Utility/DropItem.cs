using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Money,
    Exp,
    Item
}

public class DropItem : MonoBehaviour
{
    public int Count = 0;
    public ItemType DropItemType;


    //test only
    Animator animator;
    Text text;

    void Awake()
    {
        animator = GetComponent<Animator>();        
    }


    public void TestText()
    {
        text = GetComponentInChildren<Text>();
        if (text)
        {
            text.text = Count.ToString();
        }
    }

    void OnMouseDown()
    {
        //CollectItem();
        animator.Play("Test", 0);
    }

    private void CollectItem()
    {         
        switch (DropItemType)
        {
            case ItemType.Money:
                break;

            case ItemType.Exp:
                break;

            case ItemType.Item:
                break;
        }
    }
}