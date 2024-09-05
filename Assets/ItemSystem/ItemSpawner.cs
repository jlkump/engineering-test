using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemFactory ItemFactory;
    public Transform spawnPosition;
    public uint itemId;

    public void SpawnItem()
    {
        ItemInstance inst = ItemFactory.CreateItemInstance(itemId);
        inst.transform.position = spawnPosition.position;
    }
}
