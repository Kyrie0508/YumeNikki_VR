using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickupItem : MonoBehaviour
{
    public InventoryItem itemData;
    public GameObject dialogueUIPrefab;

    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnPickedUp);
    }

    void OnPickedUp(SelectEnterEventArgs args)
    {
        InventoryItem itemCopy = new InventoryItem
        {
            itemName = itemData.itemName,
            icon = itemData.icon,
            prefab = itemData.prefab
        };
        InventoryManager.Instance.AddItem(itemCopy);
        ShowDialogue(itemData.itemName + "을(를) 획득했다");
        Destroy(gameObject);
    }

    private void ShowDialogue(string text)
    {
        if (dialogueUIPrefab == null || Camera.main == null) return;

        GameObject dialogue = Instantiate(dialogueUIPrefab);
        dialogue.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
        dialogue.transform.rotation = Quaternion.LookRotation(dialogue.transform.position - Camera.main.transform.position);

        DialogueUIController controller = dialogue.GetComponent<DialogueUIController>();
        if (controller != null)
            controller.StartDialogue(text);
    }
}