using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryInstance : MonoBehaviour
{
    public uint maxSlots;
    InventoryItemSlot[] inventorySlots;
    [SerializeField]
    ItemFactory itemFactory;
    public string inventoryName;

    public delegate void InventoryUpdate();
    public event InventoryUpdate onInventoryUpdate;

    // GUIDs can be hard, just hard-code in editor for now.
    // If wanted, a system could be made for generating GUIDs based on position, instance variables, or some other consistent manner.
    public uint inventoryId = uint.MaxValue;

    void Start()
    {
        inventorySlots = new InventoryItemSlot[maxSlots];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = new InventoryItemSlot(uint.MaxValue, 0);
            inventorySlots[i].amount = 0;
        }
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
        // Put with the first matching slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].itemId == itemId && inventorySlots[i].amount + amount <= itemFactory.GetMaxStacks(itemId))
            {
                inventorySlots[i].amount += amount;
                onInventoryUpdate?.Invoke();
                return true;
            }
        }

        // Otherwise, put in the first empty slot
        for (int i = 0; i <= inventorySlots.Length; i++)
        {
            if (inventorySlots[i].amount == 0)
            {
                inventorySlots[i].itemId = itemId;
                inventorySlots[i].amount = amount;
                onInventoryUpdate?.Invoke();
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

    public InventoryItemSlot GetSlot(int slotIndex)
    {
        return inventorySlots[slotIndex];
    }

    public InventoryItemSlot[] GetSlots()
    {
        return inventorySlots;
    }

    public InventoryItemSlot SetSlot(int slotIndex, InventoryItemSlot slotItem)
    {
        InventoryItemSlot previous = inventorySlots[slotIndex];
        inventorySlots[slotIndex] = slotItem;
        return previous;
    }

    public uint GetInventoryID()
    {
        return inventoryId;
    }

    public void Save(System.IO.BinaryWriter writer)
    {
        writer.Write(inventorySlots.Length);
        print("[Save] Saving inventory... Inventory Item Length: " + inventorySlots.Length);
        foreach (InventoryItemSlot slot in inventorySlots)
        {
            print("[Save] Saving inventory... Saving slot (item id: " + slot.itemId + ", amount: " + slot.amount + ")");
            writer.Write(slot.itemId);
            writer.Write(slot.amount);
        }
    }

    public void Load(System.IO.BinaryReader reader)
    {

        int numSavedItems = reader.ReadInt32();
        print("[Load] Loading inventory... Inventory Saved Length: " + numSavedItems);
        for (int i = 0; i < numSavedItems; i++)
        {
            uint itemId = reader.ReadUInt32();
            uint amount = reader.ReadUInt32();
            print("[Load] Loading inventory... Loading slot (item id: " + itemId + ", amount: " + amount + ")");
            if (i < inventorySlots.Length)
            {
                inventorySlots[i] = new InventoryItemSlot(itemId, amount);
            } 
            else
            {
                // Change in inventory length based on the save data, drop excess on the ground
                print("[Load] Loading inventory... Encountered excess items");
                for (uint j = 0; j < amount; j++)
                {
                    ItemInstance inst = itemFactory.CreateItemInstance(itemId);
                    inst.transform.position = transform.position + transform.up * 4f;
                }
            }
        }
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

    public bool IsEmpty()
    {
        return amount == 0;
    }

    public static InventoryItemSlot MakeEmpty()
    {
        return new InventoryItemSlot(uint.MaxValue, 0);
    }

}
