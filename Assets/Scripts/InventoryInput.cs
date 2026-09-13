using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private InteractionPrompt interactionPrompt;

    [Header("Input")]
    [SerializeField] private InputActionReference storeAction;

    private InventoryItem heldItem;

    public void SetHeldItem(InventoryItem item)
{
    heldItem = item;

    if (item != null)
    {
        Debug.Log("Holding: " + item.itemName);
    }
}

    private void OnEnable()
    {
        if (storeAction != null)
        {
            storeAction.action.Enable();
            storeAction.action.performed += OnStorePressed;
        }
    }

    private void OnDisable()
    {
        if (storeAction != null)
        {
            storeAction.action.performed -= OnStorePressed;
            storeAction.action.Disable();
        }
    }

    private void OnStorePressed(InputAction.CallbackContext context)
    {
        StoreHeldItem();
    }

    private void StoreHeldItem()
    {
        if (heldItem == null)
        {
            Debug.Log("NO ITEM IN HAND");
            return;
        }

        InventoryItem item = heldItem;

        // Try to add item to inventory
        bool stored = inventory.AddItem(item);

        // Inventory full
        if (!stored)
        {
            Debug.Log("INVENTORY FULL");

            if (interactionPrompt != null)
            {
                interactionPrompt.ShowMessage(
                    "Unable to store " + item.itemName +
                    "\nInventory is full"
                );
            }

            return;
        }

        // Successfully stored
        Debug.Log("STORED SUCCESSFULLY: " + item.itemName);

        if (interactionPrompt != null)
        {
            interactionPrompt.ShowMessage(
                item.itemName + " stored successfully"
            );
        }

        // Clear reference BEFORE disabling object
        heldItem = null;

        // Hide physical object
        item.gameObject.SetActive(false);

        // Update inventory UI
        inventoryUI.RefreshUI();
    }
}