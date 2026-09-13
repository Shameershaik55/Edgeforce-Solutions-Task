using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public const int SlotCount = 9;

    [System.Serializable]
    public class InventorySlot
    {
        public string itemID;
        public string itemName;
        public Sprite icon;
        public GameObject itemPrefab;

        public bool isStackable;
        public int maxStackSize;
        public int quantity;

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(itemID) || quantity <= 0;
        }

        public void SetNewItem(InventoryItem item)
        {
            itemID = item.itemID;
            itemName = item.itemName;
            icon = item.inventoryIcon;
            itemPrefab = item.itemPrefab;

            isStackable = item.isStackable;
            maxStackSize = Mathf.Max(1, item.maxStackSize);

            quantity = 1;
        }

        public void Clear()
        {
            itemID = "";
            itemName = "";
            icon = null;
            itemPrefab = null;

            isStackable = false;
            maxStackSize = 1;
            quantity = 0;
        }
    }

    [SerializeField]
    private InventorySlot[] slots = new InventorySlot[SlotCount];

    private void Awake()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (slots[i] == null)
                slots[i] = new InventorySlot();
        }
    }

    public bool AddItem(InventoryItem item)
{
    if (item == null)
    {
        Debug.LogError("Cannot add NULL item.");
        return false;
    }

    if (string.IsNullOrEmpty(item.itemID))
    {
        Debug.LogError(item.itemName + " has no Item ID!");
        return false;
    }

    if (item.itemPrefab == null)
    {
        Debug.LogError(item.itemName + " has no prefab assigned!");
        return false;
    }

    // ==========================================
    // STACKABLE ITEM
    // ==========================================
    if (item.isStackable)
    {
        // Find the existing stack of the SAME item
        for (int i = 0; i < SlotCount; i++)
        {
            if (!slots[i].IsEmpty() &&
                slots[i].isStackable &&
                slots[i].itemID == item.itemID)
            {
                // Stack has room
                if (slots[i].quantity < slots[i].maxStackSize)
                {
                    slots[i].quantity++;

                    Debug.Log(
                        "STACKED: " +
                        item.itemName +
                        " | Quantity = " +
                        slots[i].quantity +
                        " | Slot = " +
                        i
                    );

                    return true;
                }

                // ==========================================
                // STACK IS FULL → DO NOT CREATE NEW SLOT
                // ==========================================
                Debug.Log(
                    "STACK FULL: " +
                    item.itemName +
                    " | Maximum = " +
                    slots[i].maxStackSize
                );

                return false;
            }
        }

        // No existing stack → create first stack
        for (int i = 0; i < SlotCount; i++)
        {
            if (slots[i].IsEmpty())
            {
                slots[i].SetNewItem(item);

                Debug.Log(
                    "STORED NEW STACK: " +
                    item.itemName +
                    " | Quantity = 1" +
                    " | Slot = " +
                    i
                );

                return true;
            }
        }

        Debug.Log("INVENTORY FULL!");

        return false;
    }

    // ==========================================
    // NON-STACKABLE ITEM
    // ==========================================
    if (!item.isStackable)
    {
        // Same non-stackable item already exists
        for (int i = 0; i < SlotCount; i++)
        {
            if (!slots[i].IsEmpty() &&
                !slots[i].isStackable &&
                slots[i].itemID == item.itemID)
            {
                Debug.Log(
                    "DUPLICATE NOT ALLOWED: " +
                    item.itemName
                );

                return false;
            }
        }

        // Find empty slot
        for (int i = 0; i < SlotCount; i++)
        {
            if (slots[i].IsEmpty())
            {
                slots[i].SetNewItem(item);

                Debug.Log(
                    "STORED: " +
                    item.itemName +
                    " | Quantity = 1" +
                    " | Slot = " +
                    i
                );

                return true;
            }
        }

        Debug.Log("INVENTORY FULL!");

        return false;
    }

    return false;
}

    public InventorySlot GetItem(int index)
    {
        if (index < 0 || index >= SlotCount)
            return null;

        return slots[index];
    }

    public InventorySlot RemoveItem(int index)
    {
        if (index < 0 || index >= SlotCount)
            return null;

        InventorySlot slot = slots[index];

        if (slot == null || slot.IsEmpty())
            return null;

        InventorySlot removedItem = new InventorySlot
        {
            itemID = slot.itemID,
            itemName = slot.itemName,
            icon = slot.icon,
            itemPrefab = slot.itemPrefab,
            isStackable = slot.isStackable,
            maxStackSize = slot.maxStackSize,
            quantity = 1
        };

        slot.quantity--;

        if (slot.quantity <= 0)
            slot.Clear();

        return removedItem;
    }
}