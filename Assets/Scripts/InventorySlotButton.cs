using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventorySlotButton : MonoBehaviour
{
    [SerializeField] private int slotIndex;
    [SerializeField] private InventoryRetrieval retrieval;
    [SerializeField] private Button button;

    private void Reset()
    {
        button = GetComponent<Button>();
        retrieval = FindObjectOfType<InventoryRetrieval>();
    }

    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        if (retrieval == null) retrieval = FindObjectOfType<InventoryRetrieval>();
    }

    private void OnEnable()  => button.onClick.AddListener(OnClicked);
    private void OnDisable() => button.onClick.RemoveListener(OnClicked);

    private void OnClicked()
    {
        if (retrieval == null)
        {
            Debug.LogError("InventorySlotButton: retrieval is not assigned!");
            return;
        }

        retrieval.RetrieveItem(slotIndex);
    }
}