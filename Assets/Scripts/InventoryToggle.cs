using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private InputActionReference openInventoryAction;

    private void OnEnable()
    {
        openInventoryAction.action.Enable();
        openInventoryAction.action.performed += ToggleInventory;
    }

    private void OnDisable()
    {
        openInventoryAction.action.performed -= ToggleInventory;
        openInventoryAction.action.Disable();
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }
}