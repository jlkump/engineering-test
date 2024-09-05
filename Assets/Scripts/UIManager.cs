using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    InventoryUI inventoryUI;
    [SerializeField]
    ThirdPersonController playerController;
    [SerializeField]
    GameObject generalHelpDisplay, interactHelpDisplay, playerCamera, inventoryCamera;
    [SerializeField]
    Button exitButton;
    [SerializeField]
    TextMeshProUGUI gameInfoText;
    bool displayInteractHelp = false;

    public delegate void UIEvent();
    public event UIEvent onUIClosed;

    void Start()
    {
        SetInventoryUIActive(false);
        exitButton.onClick.AddListener(OnExitClicked);
        onUIClosed += () => { inventoryUI.DropHeldItem(); };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExitClicked();
        }
    }

    void OnExitClicked()
    {
        onUIClosed?.Invoke();
        SetInventoryUIActive(false);
    }

    public void DisplayInteractHelp(bool display)
    {
        displayInteractHelp = display;
        interactHelpDisplay.SetActive(displayInteractHelp && !inventoryUI.isActiveAndEnabled);
    }

    public void SetInventoryUIActive(bool active)
    {
        inventoryUI.gameObject.SetActive(active);
        generalHelpDisplay.SetActive(!active);
        interactHelpDisplay.SetActive(!active && displayInteractHelp);
        Cursor.visible = active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        playerController.cameraControlActive = !active;
        playerCamera.SetActive(!active);
        inventoryCamera.SetActive(active);
        exitButton.gameObject.SetActive(active);
    }

    public void ShowOwnerInventory(InventoryInstance owner)
    {
        SetInventoryUIActive(true);
        inventoryUI.SetDisplayInventory(owner, 1);
        inventoryUI.SetDisplayActive(false, 0);
        inventoryUI.SetDisplayActive(true, 1);
        // TODO: Zoom to the owner on the left with the camera. Show the player inventory on the right
        // Prevent movement
    }

    public void ShowInteractInventory(InventoryInstance owner, InventoryInstance interact)
    {
        SetInventoryUIActive(true);
        inventoryUI.SetDisplayInventory(interact, 0);
        inventoryUI.SetDisplayInventory(owner, 1);
        inventoryUI.SetDisplayActive(true, 0);
        inventoryUI.SetDisplayActive(true, 1);
        // TODO: Zoom to container with the camera.
        // Prevent movement

    }

    public void SetGameInfoText(string text, Color textColor)
    {
        gameInfoText.gameObject.SetActive(true);
        gameInfoText.text = text;
        gameInfoText.color = textColor;
        StartCoroutine(StopShowingGameInfo());
    }

    IEnumerator StopShowingGameInfo()
    {
        yield return new WaitForSeconds(3f);
        gameInfoText.gameObject.SetActive(false);
    }
}
