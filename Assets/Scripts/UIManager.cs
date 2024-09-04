using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    InventoryUI inventoryUI;
    [SerializeField]
    GameObject generalHelpDisplay, interactHelpDisplay;
    bool displayInteractHelp = false;

    void Start()
    {
        SetInventoryUIActive(false);
    }

    public void DisplayInteractHelp(bool display)
    {
        displayInteractHelp = display;
        interactHelpDisplay.SetActive(displayInteractHelp);
    }

    public void SetInventoryUIActive(bool active)
    {
        print("Setting inventory ui to " + active);
        inventoryUI.gameObject.SetActive(active);
        generalHelpDisplay.SetActive(!active);
        interactHelpDisplay.SetActive(!active && displayInteractHelp);
        Cursor.visible = active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ShowOwnerInventory(InventoryInstance owner)
    {
        SetInventoryUIActive(true);
        inventoryUI.SetRightDisplay(owner);
        // TODO: Zoom to the owner on the left with the camera. Show the player inventory on the right
        // Prevent movement
    }

    public void ShowInteractInventory(InventoryInstance owner, InventoryInstance interact)
    {
        SetInventoryUIActive(true);
        inventoryUI.SetLeftDisplay(owner);
        inventoryUI.SetRightDisplay(interact);
        // TODO: Zoom to container with the camera.
        // Prevent movement

    }
}
