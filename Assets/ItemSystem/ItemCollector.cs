using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    InventoryInstance inventory;

    public bool CanCollectItem(uint itemId)
    {
        return inventory.HasSpaceForItem(itemId);
    }

    public Vector3 GetCollectPosition()
    {
        return transform.position;
    }

    /// <summary>
    ///     Returns true and collects the item for the inventory if the item can be collected, otherwise returns false
    /// </summary>
    /// <param name="itemId"> The item to be collected </param>
    public bool AttemptCollect(uint itemId)
    {
        return inventory.AddItem(itemId);
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemInstance item = other.GetComponent<ItemInstance>();
        if (item != null)
        {
            item.PickupItem(this);
        }
    }
}
