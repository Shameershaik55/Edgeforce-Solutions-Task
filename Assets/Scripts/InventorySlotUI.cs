using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text quantityText;

    public void SetItem(InventoryManager.InventorySlot item)
    {
        if (item == null || item.IsEmpty())
        {
            itemIcon.enabled = false;
            itemIcon.sprite = null;
            quantityText.text = "";
            return;
        }

        itemIcon.enabled = true;
        itemIcon.sprite = item.icon;

        if (item.quantity > 1)
        {
            quantityText.text = "×" + item.quantity;
        }
        else
        {
            quantityText.text = "";
        }
    }
}