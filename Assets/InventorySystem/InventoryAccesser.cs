using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class InventoryAccesser : MonoBehaviour
{
    public UIManager ui;
    public InventoryInstance ownerInventory;
    List<InventoryInstance> interactableInventories;

    public KeyCode ownerInventoryKey = KeyCode.Tab, interactInventoryKey = KeyCode.E;

    bool ownerInventoryOpen = false, interactInventoryOpen = false;

    void Start()
    {
        interactableInventories = new List<InventoryInstance>();
    }


    private void OnTriggerEnter(Collider other)
    {
        InventoryInstance instance = other.gameObject.GetComponent<InventoryInstance>();
        if (instance != null)
        {
            interactableInventories.Add(instance);
        }
        if (interactableInventories.Count > 0)
        {
            ui.DisplayInteractHelp(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InventoryInstance instance = other.gameObject.GetComponent<InventoryInstance>();
        if (instance != null)
        {
            interactableInventories.Remove(instance);
        }
        if (interactableInventories.Count > 0)
        {
            ui.DisplayInteractHelp(false);
        }
    }

    void Update()
    {
        bool ownerInventoryKeyPressed = Input.GetKeyDown(ownerInventoryKey);
        bool interactInventoryKeyPressed = Input.GetKeyDown(interactInventoryKey);
        if (ownerInventoryKeyPressed && !interactInventoryOpen)
        {
            ToggleOwnerInventoryView();
        }
        else if (interactInventoryKeyPressed && !ownerInventoryOpen)
        {
            ToggleInteractInventoryView();
        }
        else if (ownerInventoryKeyPressed || interactInventoryKeyPressed) 
        {
            // Makes it easier to exit an open inventory as either key can close the open inventory
            if (ownerInventoryOpen)
            {
                ToggleOwnerInventoryView();
            }
            if (interactInventoryOpen)
            {
                ToggleInteractInventoryView();
            }
        }
        print("Owner inventory open " + ownerInventoryOpen + "\nInteract inventory open " + interactInventoryOpen);
    }

    void ToggleOwnerInventoryView()
    {
        if (ownerInventoryOpen)
        {
            ui.SetInventoryUIActive(false);
        }
        else
        {
            ui.ShowOwnerInventory(ownerInventory);
        }
        ownerInventoryOpen = !ownerInventoryOpen;
    }

    void ToggleInteractInventoryView()
    {
        if (interactInventoryOpen)
        {
            ui.SetInventoryUIActive(false);
            interactInventoryOpen = false;
        }
        else
        {
            // Get the inventory closest to the inventory accessor
            InventoryInstance closest = null;
            float closestDist = float.MaxValue;
            foreach (var inventory in interactableInventories)
            {
                float dist = Vector3.Distance(inventory.gameObject.transform.position, transform.position);
                if (dist < closestDist)
                {
                    closest = inventory;
                    closestDist = dist;
                }
            }

            // If there is a accessible inventory, open it
            if (closest != null)
            {
                ui.ShowInteractInventory(ownerInventory, closest);
                interactInventoryOpen = true;
            }
        }
    }
}
