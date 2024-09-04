using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    ItemUIDisplay itemDisplayPrefab;

    [SerializeField]
    GameObject leftDisplay, leftDisplayContainer, rightDisplay, rightDisplayContainer;

    InventoryItemSlot heldItem = null;

    public void SetLeftDisplay(InventoryInstance inventoryInstance)
    {
        foreach (Transform child in leftDisplayContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var slot in inventoryInstance.GetSlots())
        {

            ItemUIDisplay slotDisplay = Instantiate(itemDisplayPrefab);
            slotDisplay.SetItemDisplay(slot);
            slotDisplay.transform.parent = leftDisplayContainer.transform;
        }
    }

    public void SetRightDisplay(InventoryInstance inventoryInstance)
    {
        foreach (Transform child in rightDisplayContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var slot in inventoryInstance.GetSlots())
        {

            ItemUIDisplay slotDisplay = Instantiate(itemDisplayPrefab);
            slotDisplay.SetItemDisplay(slot);
            slotDisplay.transform.parent = rightDisplayContainer.transform;
        }
    }

    public void SetLeftDisplayActive(bool active)
    {
        leftDisplay.SetActive(active);
    }

    public void SetRightDisplayActive(bool active)
    {
        rightDisplay.SetActive(active);
    }
}
