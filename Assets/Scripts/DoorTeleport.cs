using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string nextSceneName;
    public AudioClip doorOpenSound;

    public GameObject dialogueUIPrefab;

    private string currentSceneName;
    private FadeController fadeController;
    private AudioSource audioSource;

    private void Start()
    {
        fadeController = FindAnyObjectByType<FadeController>();
        audioSource = GetComponent<AudioSource>();
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        string requiredItemName = GetRequiredItemForScene(currentSceneName);
        
        if (!string.IsNullOrEmpty(requiredItemName) && !InventoryManager.Instance.HasItem(requiredItemName))
        {
            ShowDialogue($"{requiredItemName}이 없어 이동할 수 없습니다.");
            return;
        }

        CapsuleFollower follower = other.GetComponent<CapsuleFollower>();
        if (follower != null)
            follower.canMove = false;

        StartCoroutine(TeleportSequence());
    }

    private string GetRequiredItemForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "ROAD MAP":
                return "랜턴";
            case "Basic Room Dream":
                return "식칼";
            case "class_nikki":
                return "벨";
            default:
                return null; 
        }
    }

    private IEnumerator TeleportSequence()
    {
        if (doorOpenSound != null && audioSource != null)
            audioSource.PlayOneShot(doorOpenSound);

        if (fadeController != null)
            yield return StartCoroutine(fadeController.FadeOut());

        SceneManager.LoadScene(nextSceneName);
    }

    private void ShowDialogue(string message)
    {
        if (dialogueUIPrefab == null || Camera.main == null) return;

        GameObject dialogueGO = Instantiate(dialogueUIPrefab);
        Transform cam = Camera.main.transform;

        dialogueGO.transform.position = cam.position + cam.forward * 1.5f;
        dialogueGO.transform.rotation = Quaternion.LookRotation(dialogueGO.transform.position - cam.position);
        dialogueGO.transform.localScale = Vector3.one * 0.0025f;

        DialogueUIController controller = dialogueGO.GetComponent<DialogueUIController>();
        if (controller != null)
            controller.StartDialogue(message);
    }
}