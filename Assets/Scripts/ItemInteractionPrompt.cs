using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemInteractionPrompt : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InteractionPrompt prompt;
    [SerializeField] private InventoryItem inventoryItem;

    private XRBaseInteractable interactable;
    private bool isHeld;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        if (inventoryItem == null)
            inventoryItem = GetComponent<InventoryItem>();
    }

    private void OnEnable()
    {
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
        interactable.selectEntered.AddListener(OnGrabbed);
        interactable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
        interactable.selectEntered.RemoveListener(OnGrabbed);
        interactable.selectExited.RemoveListener(OnReleased);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (isHeld)
            return;

        if (inventoryItem == null || prompt == null)
            return;

        prompt.ShowGrabPrompt(inventoryItem.itemName);
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        // Do NOT hide while the object is being held.
        if (isHeld)
            return;

        if (prompt != null)
            prompt.HidePrompt();
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isHeld = true;

        if (inventoryItem == null || prompt == null)
            return;

        prompt.ShowStorePrompt(inventoryItem.itemName);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isHeld = false;

        if (prompt != null)
            prompt.HidePrompt();
    }
}