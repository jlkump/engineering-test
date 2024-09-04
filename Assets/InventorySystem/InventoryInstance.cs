using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInstance : MonoBehaviour
{
    public uint maxSlots;
    [SerializeField]
    InventoryItemSlot[] inventorySlots;
    [SerializeField]
    ItemFactory itemFactory;


    void Start()
    {
        inventorySlots = new InventoryItemSlot[maxSlots];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = new InventoryItemSlot(0, 0);
            inventorySlots[i].amount = 0;
        }
    }

    void Update()
    {
        
    }

    public bool HasSpaceForItem(uint itemId, uint amount = 1)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // Check if empty slot is available
            if (inventorySlots[i].amount == 0)
            {
                return true;
            }
            // Check if the itemIds match. If they do, check the stack size.
            else if (inventorySlots[i].itemId == itemId && inventorySlots[i].amount + amount <= itemFactory.GetMaxStacks(itemId))
            {
                return true;
            }
        }
        return false;
    }

    public bool AddItem(uint itemId, uint amount = 1)
    {
        // Put with the first matching slot. Otherwise, put in the first empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].itemId == itemId && inventorySlots[i].amount + amount <= itemFactory.GetMaxStacks(itemId))
            {
                inventorySlots[i].amount += amount;
                return true;
            }
        }

        for (int i = 0; i <= inventorySlots.Length; i++)
        {
            if (inventorySlots[i].amount == 0)
            {
                inventorySlots[i].itemId = itemId;
                inventorySlots[i].amount = amount;
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(uint itemId, uint amount = 1)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].itemId == itemId)
            {
                if (amount > inventorySlots[i].amount)
                {
                    inventorySlots[i].amount = 0;
                } 
                else
                {
                    inventorySlots[i].amount -= amount;
                }
                break;
            }
        }
    }

    public InventoryItemSlot GetSlot(uint slot)
    {
        return inventorySlots[slot];
    }

    public InventoryItemSlot[] GetSlots()
    {
        return inventorySlots;
    }

    public InventoryItemSlot SetSlot(uint slot, InventoryItemSlot slotItem)
    {
        InventoryItemSlot previous = inventorySlots[slot];
        inventorySlots[slot] = slotItem;
        return previous;
    }
}

[System.Serializable]
public class InventoryItemSlot
{
    public uint itemId;
    public uint amount;

    public InventoryItemSlot(uint itemId, uint amount)
    {
        this.itemId = itemId;
        this.amount = amount;
    }
}
