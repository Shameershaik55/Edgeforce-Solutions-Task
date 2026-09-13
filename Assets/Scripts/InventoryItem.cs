using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [Header("Item Data")]
    public string itemID;
    public string itemName;
    public Sprite inventoryIcon;
    public GameObject itemPrefab;

    [Header("Stack Settings")]
    public bool isStackable = false;
    public int maxStackSize = 1;
}