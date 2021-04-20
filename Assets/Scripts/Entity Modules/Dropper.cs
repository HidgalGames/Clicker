using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemToDrop
{
    public GameObject ItemObject;
    public ItemType DropItemType;
    public List<int> DropCount; //Количество выпадаемых пермедтов. Если указано несколько значений, необходимо указать шанс дропа для каждого из них.
    public List<float> DropChance; //Шанс дропа предмета для каждого из указанных значений количества.

    public ItemToDrop()
    {
        ItemObject = null;
        DropItemType = ItemType.Money;
        DropCount = new List<int>();
        DropChance = new List<float>();
    }

    public void Clear()
    {
        ItemObject = null;
        DropItemType = ItemType.Money;
        DropCount.Clear();
        DropChance.Clear();
    }

    public void CopyFrom(ItemToDrop item)
    {
        ItemObject = item.ItemObject;
        DropItemType = item.DropItemType;

        DropCount = new List<int>();
        DropCount.AddRange(item.DropCount);

        DropChance = new List<float>();
        DropChance.AddRange(item.DropChance);
    }
}

public enum DropTrigger
{
    EntityDeath,
    Timer
}

public class Dropper : MonoBehaviour
{
    public DropTrigger dropTriggerType = DropTrigger.EntityDeath;
    public List<ItemToDrop> ItemsToDrop = new List<ItemToDrop>();

    void Awake()
    {
        switch (dropTriggerType)
        {
            case DropTrigger.EntityDeath:
                DamageableEntity entity = GetComponent<DamageableEntity>();
                entity.OnEntityDeathEvent += DropItems;
                break;
        }
    }

    public void DropItems()
    {
        if(ItemsToDrop != null)
        {
            foreach (ItemToDrop item in ItemsToDrop)
            {
                float value = Random.value;
                int maxCountToDrop = 0;

                for (int i = 0; i < item.DropCount.Count; i++)
                {
                    if (value < item.DropChance[i])
                    {
                        if (item.DropCount[i] > maxCountToDrop)
                        {
                            maxCountToDrop = item.DropCount[i];
                        }
                    }
                }

                if (maxCountToDrop > 0)
                {
                    DropItem droppedItem = Instantiate(item.ItemObject, 
                        this.transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-1.5f, 0.5f)), 
                        Quaternion.identity).AddComponent<DropItem>();

                    droppedItem.DropItemType = item.DropItemType;
                    droppedItem.Count = maxCountToDrop;
                    droppedItem.TestText();
                }
            }
        }
    }
}
