using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemFactory : ScriptableObject {
    [SerializeField]
    ItemInstance[] itemPrefabs;

    public string GetItemName(uint itemId)
    {
        if (itemId < itemPrefabs.Length)
        {
            return itemPrefabs[itemId].name;
        }
        else
        {
            return "Default Name";
        }
    }

    public uint GetMaxStacks(uint itemId) {
        if (itemId < itemPrefabs.Length)
        {
            return itemPrefabs[itemId].maxStacks;
        } 
        else
        {
            return uint.MaxValue;
        }
    }

    public Sprite GetDisplayImage(uint itemId)
    {
        if (itemId < itemPrefabs.Length)
        {
            return itemPrefabs[itemId].displaySprite;
        }
        else
        {
            return null;
        }
    }

    public ItemInstance CreateItemInstance(uint itemId)
    {
        ItemInstance inst = Instantiate(itemPrefabs[itemId]);
        inst.itemId = itemId;
        return inst;
    }
}