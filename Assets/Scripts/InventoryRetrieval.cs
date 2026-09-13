using UnityEngine;

public class InventoryRetrieval : MonoBehaviour
{
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private Transform itemSpawnPoint;

    public void RetrieveItem(int slotIndex)
    {
        if (inventory == null || inventoryUI == null || itemSpawnPoint == null)
        {
            Debug.LogError("InventoryRetrieval: missing references in Inspector!");
            return;
        }

        InventoryManager.InventorySlot slot = inventory.GetItem(slotIndex);

        // Empty slot is a normal state — silently ignore
        if (slot == null || slot.IsEmpty())
            return;

        if (slot.itemPrefab == null)
        {
            Debug.LogError(slot.itemName + " has no prefab assigned!");
            return;
        }

        // Remove item data from inventory
        InventoryManager.InventorySlot item = inventory.RemoveItem(slotIndex);

        if (item == null)
        {
            Debug.LogError("Failed to remove item from inventory.");
            return;
        }

        // Spawn a NEW physical copy
        GameObject spawnedItem = Instantiate(
            item.itemPrefab,
            itemSpawnPoint.position,
            itemSpawnPoint.rotation
        );

        // Ensure the spawned item is active (prefab may have been saved disabled)
        spawnedItem.SetActive(true);

        Debug.Log("SPAWNED: " + item.itemName + " → " + spawnedItem.name);

        // Update UI
        inventoryUI.RefreshUI();
    }
}