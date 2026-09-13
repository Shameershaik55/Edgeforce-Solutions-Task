using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HeldItemTracker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryInput inventoryInput;

    private InventoryItem inventoryItem;
    private XRBaseInteractable interactable;

    private void Awake()
    {
        inventoryItem = GetComponent<InventoryItem>();
        interactable = GetComponent<XRBaseInteractable>();

        // Automatically find InventoryInput in the scene
        if (inventoryInput == null)
        {
            inventoryInput = FindFirstObjectByType<InventoryInput>();
        }

        // Safety checks
        if (inventoryItem == null)
        {
            Debug.LogError(
                gameObject.name + " is missing InventoryItem component!"
            );
        }

        if (interactable == null)
        {
            Debug.LogError(
                gameObject.name + " is missing XR Grab Interactable!"
            );
        }

        if (inventoryInput == null)
        {
            Debug.LogError(
                "No InventoryInput found in the scene!"
            );
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnGrabbed);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnGrabbed);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (inventoryInput == null)
        {
            Debug.LogError("InventoryInput is NULL. Cannot store held item.");
            return;
        }

        if (inventoryItem == null)
        {
            Debug.LogError("InventoryItem is NULL on " + gameObject.name);
            return;
        }

        inventoryInput.SetHeldItem(inventoryItem);

        Debug.Log("GRABBED: " + inventoryItem.itemName);
    }
}