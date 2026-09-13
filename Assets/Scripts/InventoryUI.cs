using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private InventorySlotUI[] slots;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (inventory == null)
        {
            Debug.LogError("InventoryUI: Inventory reference missing!");
            return;
        }

        if (slots == null || slots.Length != InventoryManager.SlotCount)
        {
            Debug.LogError(
                "InventoryUI: Slots must contain exactly 9 slots!"
            );
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                Debug.LogError(
                    "InventoryUI: Slot " + i + " is missing!"
                );
                continue;
            }

            slots[i].SetItem(inventory.GetItem(i));
        }
    }
}