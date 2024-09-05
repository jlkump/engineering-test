using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    ItemUIDisplay itemDisplayPrefab;

    [SerializeField]
    InventoryDisplay[] displays;

    [SerializeField]
    UIHeldItem heldItemDisplay;

    [SerializeField]
    ItemFactory itemFactory;

    [SerializeField]
    Button dropHeldItemButton;

    [SerializeField]
    Transform itemDropLocation;

    InventoryItemSlot heldItem;

    private void Start()
    {
        heldItem = InventoryItemSlot.MakeEmpty();
        dropHeldItemButton.onClick.AddListener(OnDropItemClicked);
    }

    void OnDropItemClicked()
    {
        if (heldItem.amount != 0)
        {
            ItemInstance inst = itemFactory.CreateItemInstance(heldItem.itemId);
            inst.transform.position = itemDropLocation.position;
            inst.SetPickupDelay(4f);
            if (heldItem.amount == 1)
            {
                heldItem = InventoryItemSlot.MakeEmpty();
                print("Setting held item to (" + heldItem.itemId + ", " + heldItem.amount + ")");
            } 
            else
            {
                heldItem.amount--;
            }
            heldItemDisplay.SetItem(heldItem, itemFactory);
        }
    }

    void ItemSlotOnclick(ItemUIDisplay itemSlot, InventoryDisplay display)
    {
        itemSlot.SetItemDisplay(heldItem);
        InventoryItemSlot previous = display.inventory.SetSlot(itemSlot.slotIndex, heldItem);
        print("Item slot clicked, swapping (" + heldItem.itemId + ", " + heldItem.amount + ")" + " with (" + previous.itemId + ", " + previous.amount + ")");
        heldItem = previous;
        heldItemDisplay.SetItem(heldItem, itemFactory);

    }

    public void SetDisplayInventory(InventoryInstance inventoryInstance, int displayId)
    {
        displays[displayId].inventory = inventoryInstance;
        displays[displayId].titleText.SetText(inventoryInstance.inventoryName);
        foreach (Transform child in displays[displayId].gridDisplayContainer.transform)
        {
            Destroy(child.gameObject);
        }
        InventoryItemSlot[] slots = inventoryInstance.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            InventoryItemSlot slot = slots[i];
            ItemUIDisplay slotDisplay = Instantiate(itemDisplayPrefab);
            slotDisplay.SetItemDisplay(slot);
            slotDisplay.transform.parent = displays[displayId].gridDisplayContainer.transform;
            slotDisplay.slotIndex = i;
            slotDisplay.button.onClick.AddListener(() =>
            {
                ItemSlotOnclick(slotDisplay, displays[displayId]);
            });
        }
    }

    public void SetDisplayActive(bool active, int displayId)
    {
        displays[displayId].rootDisplay.SetActive(active);
    }

    public void SetWatchInventory(InventoryInstance inventoryInstance)
    {
        inventoryInstance.onInventoryUpdate += UpdateInventoryDisplay;
    }

    void UpdateInventoryDisplay()
    {
        for (int i = 0; i < displays.Length; i++)
        {
            Transform parent = displays[i].gridDisplayContainer.transform;
            for (int j = 0; j < parent.childCount; j++)
            {
                ItemUIDisplay itemDisplay = parent.GetChild(j).GetComponent<ItemUIDisplay>();
                if (itemDisplay != null)
                {
                    itemDisplay.SetItemDisplay(displays[i].inventory.GetSlot(i));
                }
            }
        }
    }
}

[System.Serializable]
public class InventoryDisplay
{
    public GameObject rootDisplay, gridDisplayContainer;
    public TextMeshProUGUI titleText;

    [HideInInspector]
    public InventoryInstance inventory;
}