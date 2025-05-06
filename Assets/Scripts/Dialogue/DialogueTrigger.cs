using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueUIPrefab;
    private GameObject dialogueUIInstance;

    private void OnEnable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.AddListener(OnSelect);
    }

    private void OnDisable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.RemoveListener(OnSelect);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        
        if (dialogueUIInstance == null)
        {
            dialogueUIInstance = Instantiate(dialogueUIPrefab);

            
            Canvas canvas = dialogueUIInstance.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.worldCamera = Camera.main;
            }
        }

        Transform cam = Camera.main.transform;
        dialogueUIInstance.transform.position = cam.position + cam.forward * 2.0f;
        dialogueUIInstance.transform.LookAt(cam);
        dialogueUIInstance.transform.rotation = Quaternion.LookRotation(dialogueUIInstance.transform.position - cam.position);

        dialogueUIInstance.GetComponent<DialogueUIController>().StartDialogue("이건 뭔가 수상한 느낌이야...");
    }
}