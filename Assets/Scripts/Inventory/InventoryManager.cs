using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory UI")]
    public GameObject inventoryUI;                // Canvas_InventoryUI
    public Transform cameraTransform;
    public Vector3 offsetFromCamera = new Vector3(0, -0.2f, 2.5f);
    public float uiScale = 0.02f;

    [Header("Slot Settings")]
    public Transform slotContainer;
    public GameObject slotPrefab;

    [Header("Item Equip")]
    public Transform itemHolder;

    private List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private bool isVisible = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddItem(InventoryItem item)
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);

            GameObject slotGO = Instantiate(slotPrefab, slotContainer);
            Image icon = slotGO.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI name = slotGO.transform.Find("Name").GetComponent<TextMeshProUGUI>();

            icon.sprite = item.icon;
            name.text = item.itemName;

            Button btn = slotGO.GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(() => EquipItem(item));
            Debug.Log("Add Item");
        }
    }

    public void EquipItem(InventoryItem item)
    {
        ClearCurrentItem();

        if (item.prefab != null && itemHolder != null)
        {
            Instantiate(item.prefab, itemHolder.position, itemHolder.rotation, itemHolder);
        }
    }

    public void ClearCurrentItem()
    {
        foreach (Transform child in itemHolder)
            Destroy(child.gameObject);
    }

    public void ToggleInventory()
    {
        isVisible = !isVisible;
        inventoryUI.SetActive(isVisible);

        if (isVisible)
        {
            inventoryUI.transform.position = cameraTransform.position +
                cameraTransform.forward * offsetFromCamera.z +
                cameraTransform.up * offsetFromCamera.y;
            inventoryUI.transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
            inventoryUI.transform.localScale = Vector3.one * uiScale;
        }
    }
    
    public bool HasItem(string itemName)
    {
        return inventoryItems.Any(item => item.itemName == itemName);
    }
}
