using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueTrigger : MonoBehaviour
{
    [Header("UI 설정")]
    public GameObject dialogueUIPrefab;
    public Vector3 offsetFromCamera = new Vector3(0, 0, 2f);

    [Header("대사 설정")]
    [TextArea(2, 10)]
    public string[] dialogueLines;
    public bool playOnSelect = true;

    private GameObject dialogueUIInstance;
    private XRBaseInteractable interactable;

    private void OnEnable()
    {
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null && playOnSelect)
            interactable.selectEntered.AddListener(OnSelect);
    }

    private void OnDisable()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnSelect);
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        if (dialogueUIPrefab == null || Camera.main == null)
            return;

        if (dialogueUIInstance == null)
        {
            dialogueUIInstance = Instantiate(dialogueUIPrefab);

            Canvas canvas = dialogueUIInstance.GetComponent<Canvas>();
            if (canvas != null)
                canvas.worldCamera = Camera.main;
        }

        Transform cam = Camera.main.transform;
        Vector3 pos = cam.position + cam.TransformDirection(offsetFromCamera) * 2.0f;
        dialogueUIInstance.transform.position = pos;
        dialogueUIInstance.transform.LookAt(cam);
        dialogueUIInstance.transform.rotation = Quaternion.LookRotation(dialogueUIInstance.transform.position - cam.position);

        DialogueUIController controller = dialogueUIInstance.GetComponent<DialogueUIController>();
        if (controller != null)
            controller.StartDialogue(dialogueLines);
    }
}