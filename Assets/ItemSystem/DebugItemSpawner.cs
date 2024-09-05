using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItemSpawner : MonoBehaviour
{
    [SerializeField]
    ItemSpawner itemSpawner;

    KeyCode item0 = KeyCode.Alpha0, item1 = KeyCode.Alpha1;

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
    }
}
