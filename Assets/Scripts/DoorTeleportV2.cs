using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleportV2 : MonoBehaviour
{
    public string nextSceneName;
    public AudioClip doorOpenSound;
    public Transform lieDownViewTransform;       // ✅ 원하는 시점 위치 (천장 바라보는 위치)
    public Transform xrOriginTransform;          // ✅ XR Origin (XR Rig) 오브젝트
    public float waitBeforeSound = 3f;           // ✅ 시점 유지 시간
    public float fadeDelay = 0.5f;               // ✅ 사운드 후 페이드까지 대기 시간

    private FadeController fadeController;
    private AudioSource audioSource;

    private bool hasTriggered = false;

    private void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        audioSource = GetComponent<AudioSource>();
        Debug.Log("[DoorTeleportV2] 초기화 완료.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        hasTriggered = true;

        Debug.Log($"[DoorTeleportV2] 충돌 감지: {other.name}");

        if (other.CompareTag("Player") || other.CompareTag("Untagged"))
        {
            // ✅ 플레이어 이동 제한
            CapsuleFollower follower = other.GetComponent<CapsuleFollower>();
            if (follower != null)
            {
                follower.canMove = false;
            }

            // ✅ XR Origin 이동 → Main Camera 대신 XR Rig 이동
            if (xrOriginTransform != null && lieDownViewTransform != null)
            {
                xrOriginTransform.position = lieDownViewTransform.position;
                xrOriginTransform.rotation = lieDownViewTransform.rotation;
                Debug.Log("[DoorTeleportV2] XR Origin 위치 이동 완료.");
            }

            StartCoroutine(WaitAndTeleport());
        }
    }

    private IEnumerator WaitAndTeleport()
    {
        // ✅ 1. 3초 대기 (시점 유지)
        yield return new WaitForSeconds(waitBeforeSound);

        // ✅ 2. 문 소리 재생
        if (doorOpenSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }

        // ✅ 3. 약간 대기 후 페이드 시작
        yield return new WaitForSeconds(fadeDelay);

        // ✅ 4. 페이드 아웃
        if (fadeController != null)
        {
            yield return StartCoroutine(fadeController.FadeOut());
        }

        // ✅ 5. 씬 전환
        SceneManager.LoadScene(nextSceneName);
    }
}
