using StarterAssets;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    ItemFactory itemFactory;

    [SerializeField]
    UIManager uiManager;

    public KeyCode newGameKey = KeyCode.N, saveGameKey = KeyCode.K, loadGameKey = KeyCode.L;

    string savePath;

    static GameManager inst = null;
    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveFile");
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKey(newGameKey))
        {
            BeginNewGame();
        } 
        else if (Input.GetKey(saveGameKey))
        {
            SaveGame();
        }
        else if (Input.GetKey(loadGameKey))
        {
            LoadGame();
        }
    }

    void BeginNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadGame()
    {
        BeginNewGame();

        print("Load Pressed");
        ItemInstance[] items = FindObjectsOfType<ItemInstance>();
        foreach (ItemInstance item in items)
        {
            Destroy(item.gameObject); // Destroy possible duplicates
        }
        StartCoroutine(DelayLoad());
    }

    // Delayed to let newly instanced items perform awake and update
    IEnumerator DelayLoad()
    {
        print("Delaying load");
        yield return new WaitForFixedUpdate();
        if (uiManager != null)
        {
            uiManager.SetGameInfoText("Loading from save...", Color.green);
        }
        using (
           BinaryReader reader = new BinaryReader(File.Open(savePath, FileMode.Open))
        )
        {
            print("Loading from file...");
            
            // Load player position
            Transform playerTransform = FindObjectOfType<ThirdPersonController>().transform;
            float xPos = reader.ReadSingle();
            float yPos = reader.ReadSingle();
            float zPos = reader.ReadSingle();
            print("[Load] Saved player transform position: " + xPos + ", " + yPos + ", " + zPos);
            playerTransform.gameObject.GetComponent<CharacterController>().enabled = false;
            playerTransform.position = new Vector3(xPos, yPos, zPos);


            // Load Inventories by Id
            int numInventoriesSaved = reader.ReadInt32();
            print("[Load] Loading inventories... Found " + numInventoriesSaved + " number of inventories.");
            InventoryInstance[] inventories = FindObjectsOfType<InventoryInstance>();

            for (int i = 0; i < numInventoriesSaved; i++)
            {
                uint inventoryId = reader.ReadUInt32();
                // Find the inventory that matches by Id
                foreach (var inventory in inventories)
                {
                    if (inventory.inventoryId == inventoryId)
                    {
                        inventory.Load(reader);
                    }
                }
            }

            // Load Item Instances
            int numSavedItems = reader.ReadInt32();
            print("[Load] Loading items... Found " + numSavedItems + " number of items.");
            for (int i = 0; i < numSavedItems; i++)
            {
                uint itemId = reader.ReadUInt32();
                print("[Load] Loading item... Current Item " + itemId);
                ItemInstance inst = itemFactory.CreateItemInstance(itemId);
                inst.Load(reader);
            }
        }

        yield return new WaitForFixedUpdate();
        FindObjectOfType<ThirdPersonController>().transform.gameObject.GetComponent<CharacterController>().enabled = true;

    }

    void SaveGame()
    {
        if (uiManager != null)
        {
            uiManager.SetGameInfoText("Saving game...", Color.green);
        }
        // Write raw binary data to a save file. (Using ensures save file closing).
        using (
            BinaryWriter writer = new BinaryWriter(File.Open(savePath, FileMode.Create))
        ) 
        {
            print("Saving to file...");
            // Save player position
            Transform playerTransform = FindObjectOfType<ThirdPersonController>().transform;
            writer.Write(playerTransform.position.x);
            writer.Write(playerTransform.position.y);
            writer.Write(playerTransform.position.z);
            print("[Save] Saved player transform position: " + playerTransform.position.x + ", " + playerTransform.position.y + ", " + playerTransform.position.z);

            // Save Inventories
            InventoryInstance[] inventories = FindObjectsOfType<InventoryInstance>();
            writer.Write(inventories.Length);
            print("[Save] Saving inventories... Found " + inventories.Length + " number of inventories.");
            foreach (var inventory in inventories)
            {
                writer.Write(inventory.GetInventoryID());
                print("[Save] Saving inventories... Current Inventory " + inventory.GetInventoryID());
                inventory.Save(writer);
            }

            // Save Item Instances
            ItemInstance[] items = FindObjectsOfType<ItemInstance>();
            writer.Write(items.Length);
            print("[Save] Saving items... Found " + items.Length + " number of items.");
            foreach (var item in items)
            {
                writer.Write(item.itemId);
                print("[Save] Saving item... Current Item " + item.itemId);
                item.Save(writer);
            }
        }
    }
}
