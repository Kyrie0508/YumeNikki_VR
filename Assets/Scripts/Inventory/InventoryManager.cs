using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory UI")]
    public GameObject inventoryUI;                
    public Transform cameraTransform;
    public Vector3 offsetFromCamera = new Vector3(0, -0.2f, 2.5f);
    public float uiScale = 0.02f;

    [Header("Slot Settings")]
    public Transform slotContainer;
    public GameObject slotPrefab;

    [Header("Item Equip")]
    public Transform rightHandTransform; 
    public AudioClip equipSound;
    public AudioSource audioSource; 

    
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private InventoryItem currentEquippedItem;
    private bool isVisible = false;

    
    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void AddItem(InventoryItem item)
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
            CreateSlotUI(item);
        }
    }

    void CreateSlotUI(InventoryItem item)
    {
        if (slotContainer == null) return;
        
        GameObject slotGO = Instantiate(slotPrefab, slotContainer);
        Image icon = slotGO.transform.Find("Icon").GetComponent<Image>();
        TextMeshProUGUI name = slotGO.transform.Find("Name").GetComponent<TextMeshProUGUI>();

        icon.sprite = item.icon;
        name.text = item.itemName;

        Button btn = slotGO.transform.Find("Icon").GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => EquipItem(item));
        }
    }

    public void EquipItem(InventoryItem item)
    {
        ClearCurrentItem();

        if (item.prefab != null && rightHandTransform != null)
        {
            if (item.itemName == "식칼")
            {
                GameObject equipped = Instantiate(item.prefab, rightHandTransform);
                equipped.transform.localPosition = Vector3.zero;
                equipped.transform.localRotation = Quaternion.Euler(45, 0, 90);
                equipped.transform.localScale = Vector3.one * 2.0f;

                Collider col = equipped.GetComponentInChildren<Collider>();
                if (col != null) col.isTrigger = true;

                Rigidbody rb = equipped.GetComponentInChildren<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                currentEquippedItem = item;
            }

            if (audioSource != null && equipSound != null)
                audioSource.PlayOneShot(equipSound);
        }
    }

    public void ClearCurrentItem()
    {
        foreach (Transform child in rightHandTransform)
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inventoryUI = GameObject.Find("Canvas_InventoryUI");
        slotContainer = GameObject.Find("SlotContainer")?.transform;
        rightHandTransform = GameObject.Find("Fingers2R")?.transform;
        cameraTransform = Camera.main?.transform;

        if (slotContainer != null)
        {
            foreach (Transform child in slotContainer)
                Destroy(child.gameObject);

            foreach (var item in inventoryItems)
                CreateSlotUI(item);
        }

        if (currentEquippedItem != null)
        {
            EquipItem(currentEquippedItem);
        }

        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
            isVisible = false;
        }
    }
}