using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string nextSceneName;
    public AudioClip doorOpenSound;

    private FadeController fadeController;
    private AudioSource audioSource;

    private void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        audioSource = GetComponent<AudioSource>();
        Debug.Log("[DoorTeleport] 초기화 완료.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[DoorTeleport] 충돌 감지: {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("[DoorTeleport] 플레이어와 충돌 감지됨. 이동 제한 및 씬 전환 시퀀스 시작.");

            // CapsuleFollower 끄기 (이동 제한)
            CapsuleFollower follower = other.GetComponent<CapsuleFollower>();
            if (follower != null)
            {
                follower.canMove = false;
                Debug.Log("[DoorTeleport] 플레이어 이동 비활성화 완료.");
            }

            StartCoroutine(TeleportSequence());
        }
    }

    private IEnumerator TeleportSequence()
    {
        if (doorOpenSound != null && audioSource != null)
        {
            Debug.Log("[DoorTeleport] 문 여는 사운드 재생.");
            audioSource.PlayOneShot(doorOpenSound);
        }

        if (fadeController != null)
        {
            Debug.Log("[DoorTeleport] 페이드 아웃 시작.");
            yield return StartCoroutine(fadeController.FadeOut());
        }

        Debug.Log($"[DoorTeleport] 씬 전환 시작: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName);
    }
}
