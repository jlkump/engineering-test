using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIDisplay : MonoBehaviour
{
    [SerializeField]
    Image itemDisplay;
    [SerializeField]
    TextMeshProUGUI nameText, amountText;

    [SerializeField]
    Sprite defaultDisplaySprite;

    [SerializeField]
    ItemFactory itemFactory;

    public Button button;
    public int slotIndex = -1;


    public void SetItemDisplay(InventoryItemSlot item)
    {
        if (item.amount == 0)
        {
            // Display empty
            itemDisplay.gameObject.SetActive(false);
            nameText.SetText("");
            amountText.SetText("");
        }
        else
        {
            // Display the item icon
            itemDisplay.gameObject.SetActive(true);
            Sprite displayImage = itemFactory.GetDisplayImage(item.itemId);
            if (displayImage != null)
            {
                itemDisplay.sprite = displayImage;
            } 
            else
            {
                itemDisplay.sprite = defaultDisplaySprite;
            }
            nameText.SetText(itemFactory.GetItemName(item.itemId));
            amountText.SetText("x" +  item.amount);
        }
    }

    void ButtonClicked()
    {
        print("Button clicked!");
    }
}
