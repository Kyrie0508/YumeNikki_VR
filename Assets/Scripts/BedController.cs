using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using System.Collections;

public class BedController : MonoBehaviour
{
    public XROrigin xrOrigin;                     // XR Origin 참조
    public Transform bedLyingPosition;            // 눕는 위치 (위치 + 회전 포함)
    public FadeController fadeController;         // 페이드 아웃 컨트롤러
    public AudioSource bellSound;                 // 종소리
    public string nextSceneName = "NextScene";    // 다음 씬 이름

    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated) return; // 중복 방지

        if (other.CompareTag("Player"))
        {
            hasActivated = true;
            StartCoroutine(BedSequence());
        }
    }

    private IEnumerator BedSequence()
    {
        Debug.Log("🛏 침대 시퀀스 시작");

        // XR Origin 위치 이동 및 회전 → 눕고 하늘 보기
        xrOrigin.transform.SetPositionAndRotation(
            bedLyingPosition.position,
            Quaternion.Euler(90f, bedLyingPosition.eulerAngles.y, 0f)
        );

        // (선택) 움직임 제한 - 필요시 CapsuleFollower 등에서 이동 막기
        CapsuleFollower follower = xrOrigin.GetComponentInChildren<CapsuleFollower>();
        if (follower != null) follower.canMove = false;

        // 3초 대기
        yield return new WaitForSeconds(3f);

        // Fade Out
        if (fadeController != null)
            yield return StartCoroutine(fadeController.FadeOut());

        // 종소리 재생
        if (bellSound != null)
            bellSound.Play();

        // 종소리 길이만큼 대기 (or 1초)
        yield return new WaitForSeconds(bellSound.clip.length);

        // 다음 씬 로드
        SceneManager.LoadScene(nextSceneName);
    }
}
