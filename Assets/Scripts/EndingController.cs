using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndingController : MonoBehaviour
{
    public GameObject player;
    public FadeController fadeController;
    public AudioSource windAudioSource;
    public AudioSource impactAudioSource;
    public GameObject creditUI;

    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();

        // 충돌음이 2D 사운드로 들리도록 설정
        impactAudioSource.spatialBlend = 0f;

        StartCoroutine(PlayEndingSequence());
    }

    private IEnumerator PlayEndingSequence()
    {
        // 1. 플레이어 이동 제한
        CapsuleFollower follower = player.GetComponent<CapsuleFollower>();
        if (follower != null) follower.canMove = false;

        // 2. 잠깐 정적 (2초)
        yield return new WaitForSeconds(2f);

        // 3. 위로 살짝 떠오르기 (1m, 1.5초)
        yield return StartCoroutine(MovePlayerOffset(new Vector3(0f, 1f, 0f), 1.5f));

        // 4. 앞으로 전진하기 (3m, 2초)
        yield return StartCoroutine(MovePlayerOffset(new Vector3(0f, 0f, 3f), 2f));

        // 5. 정지 후 잠시 정적 (1초)
        yield return new WaitForSeconds(1f);

        // 6. 중력 활성화 → 낙하 시작
        playerRb.useGravity = true;
        playerRb.isKinematic = false;

        // 바람 소리 재생
        windAudioSource.volume = 1f;
        windAudioSource.Play();

        // 7. 5초 후 페이드아웃 시작
        yield return new WaitForSeconds(5f);
        StartCoroutine(fadeController.FadeOut());

        // 바람 소리 서서히 줄이기
        yield return StartCoroutine(FadeOutAudio(windAudioSource, 1f));

        // 8. 3초 후 충돌음
        yield return new WaitForSeconds(3f);
        impactAudioSource.enabled = true;
        impactAudioSource.Play();

        // 9. 5초 정적
        yield return new WaitForSeconds(5f);

        // 10. 크레딧 UI 표시 + 애니메이션 재생
        creditUI.SetActive(true);
        Animator animator = creditUI.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("StartCredit");
        }

        // 11. 크레딧 재생 후 15초 뒤에 타이틀 씬으로 전환
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("Title"); // ← 씬 이름 정확히 입력
    }

    // 📌 플레이어 위치를 일정 시간 동안 offset만큼 이동시키는 함수
    private IEnumerator MovePlayerOffset(Vector3 offset, float duration)
    {
        Vector3 start = player.transform.position;
        Vector3 end = start + offset;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            player.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        player.transform.position = end;
    }

    // 📌 AudioSource 페이드아웃 (볼륨 서서히 줄이기)
    private IEnumerator FadeOutAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // 다음 재생을 위해 복원
    }
}
