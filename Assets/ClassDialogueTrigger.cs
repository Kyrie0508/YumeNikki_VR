using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ClassDialogueTrigger : MonoBehaviour
{
    [Header("UI 설정")]
    public GameObject dialogueUIPrefab;
    public Vector3 offsetFromCamera = new Vector3(0, 0, 2f);

    [Header("대사 설정")]
    [TextArea(2, 5)]
    public string[] dialogueLines = { "모든 아이템을 모았습니다...", "이제 이곳을 떠날 수 있습니다." };

    private GameObject dialogueUIInstance;

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
        Vector3 pos = cam.position + cam.TransformDirection(offsetFromCamera);
        dialogueUIInstance.transform.position = pos;
        dialogueUIInstance.transform.LookAt(cam);
        dialogueUIInstance.transform.rotation = Quaternion.LookRotation(dialogueUIInstance.transform.position - cam.position);

        DialogueUIController controller = dialogueUIInstance.GetComponent<DialogueUIController>();
        if (controller != null)
        {
            controller.StartDialogue(dialogueLines);
            controller.OnDialogueComplete += () => 
            {
                SceneManager.LoadScene("Ending Outside");
            };
        }
    }
}