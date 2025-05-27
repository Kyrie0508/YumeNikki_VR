using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string nextSceneName;
    public AudioClip doorOpenSound;
    public string requiredItemName = "식칼";

    public GameObject dialogueUIPrefab; 

    private FadeController fadeController;
    private AudioSource audioSource;

    private void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 아이템 보유 여부 확인
            if (!InventoryManager.Instance.HasItem(requiredItemName))
            {
                ShowDialogue($"{requiredItemName}이(가) 없어 이동할 수 없습니다.");
                return;
            }

            CapsuleFollower follower = other.GetComponent<CapsuleFollower>();
            if (follower != null)
                follower.canMove = false;

            StartCoroutine(TeleportSequence());
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

        dialogueGO.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
        dialogueGO.transform.rotation = Quaternion.LookRotation(dialogueGO.transform.position - cam.position);
        dialogueGO.transform.localScale = Vector3.one * 0.0025f;

        DialogueUIController controller = dialogueGO.GetComponent<DialogueUIController>();
        if (controller != null)
            controller.StartDialogue(message);
    }
}
