using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItemSpawner : MonoBehaviour
{
    [SerializeField]
    ItemSpawner itemSpawner;

    KeyCode item0 = KeyCode.Alpha0, item1 = KeyCode.Alpha1, item2 = KeyCode.Alpha2, 
        item3 = KeyCode.Alpha3, item4 = KeyCode.Alpha4, item5 = KeyCode.Alpha5, item6 = KeyCode.Alpha6,
        item7 = KeyCode.Alpha7;

    public void Update()
    {
        if (Input.GetKeyDown(item0))
        {
            itemSpawner.itemId = 0;
            itemSpawner.SpawnItem();
        }

        if (Input.GetKeyDown(item1))
        {
            itemSpawner.itemId = 1;
            itemSpawner.SpawnItem();
        }

        if (Input.GetKeyDown(item2))
        {
            itemSpawner.itemId = 2;
            itemSpawner.SpawnItem();
        }

        if (Input.GetKeyDown(item3))
        {
            itemSpawner.itemId = 3;
            itemSpawner.SpawnItem();
        }

        if (Input.GetKeyDown(item4))
        {
            itemSpawner.itemId = 4;
            itemSpawner.SpawnItem();
        }

        if (Input.GetKeyDown(item5))
        {
            itemSpawner.itemId = 5;
            itemSpawner.SpawnItem();
        }

        if (Input.GetKeyDown(item6))
        {
            itemSpawner.itemId = 6;
            itemSpawner.SpawnItem();
        }

        if (Input.GetKeyDown(item7))
        {
            itemSpawner.itemId = 7;
            itemSpawner.SpawnItem();
        }
    }
}
