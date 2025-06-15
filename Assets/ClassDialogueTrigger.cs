using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClassDialogueTrigger : MonoBehaviour
{
    [Header("UI 설정")]
    public GameObject dialogueUIPrefab;
    public Vector3 offsetFromCamera = new Vector3(0, 0, 2f);

    [Header("대사 설정")]
    [TextArea(2, 5)]
    public string dialogueText = "... ... ...";
    public bool playOnSelect = true;

    private GameObject dialogueUIInstance;
    private XRBaseInteractable interactable;

    public void ShowDialogue()
    {

        if (dialogueUIPrefab == null || Camera.main == null)
        {
            Debug.LogWarning("dialogueUIPrefab 또는 Camera.main 이 null입니다.");
        }

        if (dialogueUIInstance == null)
        {
            dialogueUIInstance = Instantiate(dialogueUIPrefab);

            Canvas canvas = dialogueUIInstance.GetComponent<Canvas>();
            if (canvas != null)
                canvas.worldCamera = Camera.main;
        }

        Transform cam = Camera.main.transform;
        Vector3 pos = cam.position + cam.TransformDirection(offsetFromCamera);
        dialogueUIInstance.transform.position = pos;
        dialogueUIInstance.transform.LookAt(cam);
        dialogueUIInstance.transform.rotation = Quaternion.LookRotation(dialogueUIInstance.transform.position - cam.position);

        DialogueUIController controller = dialogueUIInstance.GetComponent<DialogueUIController>();
        if (controller != null)
        {
            controller.StartDialogue(dialogueText);
        }
        else
        {
            Debug.LogWarning("DialogueUIController 컴포넌트가 없습니다!");
        }
    }
}