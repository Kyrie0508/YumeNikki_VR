using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public InventoryItem item;
    public Button button;

    public void Initialize(InventoryItem newItem)
    {
        item = newItem;
        GetComponent<Image>().sprite = item.icon;

        if (button == null)
            button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => InventoryManager.Instance.EquipItem(item));
    }
}