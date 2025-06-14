using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class StopMoveOnCollision : MonoBehaviour
{
    [Header("References")]
    public ActionBasedContinuousMoveProvider moveProvider;  // Inspector에서 할당

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // 벽 태그를 “Wall”로 지정했다면
        if (hit.gameObject.CompareTag("Wall"))
        {
            // 1) Move Provider 완전 비활성화
            moveProvider.enabled = false;

            // 2) (선택) 속도만 0으로 조절하고 싶으면:
            // moveProvider.moveSpeed = 0f;

            Debug.Log("🚧 충돌 감지 – 이동 중지");
        }
    }
}
