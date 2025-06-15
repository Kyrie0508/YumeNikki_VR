using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string nextSceneName;              // 이동할 씬 이름
    public string targetSpawnPointName;       // 다음 씬에서 리스폰할 지점 이름
    public AudioClip doorOpenSound;
    public GameObject dialogueUIPrefab;

    private FadeController fadeController;
    private AudioSource audioSource;

    private void Start()
    {
        fadeController = FindAnyObjectByType<FadeController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // CapsuleFollower 비활성화 (선택사항)
        CapsuleFollower follower = other.GetComponent<CapsuleFollower>();
        if (follower != null)
            follower.canMove = false;

        StartCoroutine(TeleportSequence());
    }

    private IEnumerator TeleportSequence()
    {
        // 문 열리는 사운드
        if (doorOpenSound != null && audioSource != null)
            audioSource.PlayOneShot(doorOpenSound);

        // 페이드 아웃
        if (fadeController != null)
            yield return StartCoroutine(fadeController.FadeOut());

        // 현재 씬 이름과 다음 씬 리스폰 위치 저장
        SceneTransitionManager.LastSceneName = SceneManager.GetActiveScene().name;
        SceneTransitionManager.NextSpawnPointName = targetSpawnPointName;

        // 씬 이동
        SceneManager.LoadScene(nextSceneName);
    }

    private void ShowDialogue(string[] lines)
    {
        if (dialogueUIPrefab == null || Camera.main == null) return;

        GameObject dialogueGO = Instantiate(dialogueUIPrefab);
        Transform cam = Camera.main.transform;

        dialogueGO.transform.position = cam.position + cam.forward * 1.5f;
        dialogueGO.transform.rotation = Quaternion.LookRotation(dialogueGO.transform.position - cam.position);
        dialogueGO.transform.localScale = Vector3.one * 0.0025f;

        DialogueUIController controller = dialogueGO.GetComponent<DialogueUIController>();
        if (controller != null)
            controller.StartDialogue(lines);
    }
}
