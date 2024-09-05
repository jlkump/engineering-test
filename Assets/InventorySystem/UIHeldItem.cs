using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHeldItem : MonoBehaviour
{
    [SerializeField]
    Vector3 offset = Vector3.zero;

    [SerializeField]
    TextMeshProUGUI amountText;

    void Update()
    {
        transform.position = Input.mousePosition + offset;
    }

    public void SetItem(InventoryItemSlot item, ItemFactory itemFactory)
    {
        if (item.IsEmpty())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            GetComponent<Image>().sprite = itemFactory.GetDisplayImage(item.itemId);
            amountText.SetText("x" + item.amount);
        }

    }
}
