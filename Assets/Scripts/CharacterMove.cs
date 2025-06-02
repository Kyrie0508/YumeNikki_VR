using UnityEngine;

public class VRAvatarFollow : MonoBehaviour
{
    public Transform xrOrigin;             // XR Rig
    public Transform mainCamera;           // Main Camera
    public Transform headBone;             // 캐릭터의 Head
    public Transform leftHandTarget;       // 왼손 컨트롤러
    public Transform rightHandTarget;      // 오른손 컨트롤러
    public Transform leftHandBone;         // 캐릭터 왼손
    public Transform rightHandBone;        // 캐릭터 오른손

    private Quaternion leftHandCorrection = Quaternion.Euler(0f, 270f, -90f);
    private Quaternion rightHandCorrection = Quaternion.Euler(0f, -270f, 90f);

    void Start()
    {
        // Rigidbody가 있으면 물리 반응 제거
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        // 1. 카메라와 캐릭터 머리 차이 계산 (Y축 제거)
        Vector3 offset = mainCamera.position - headBone.position;
        offset.y = 0f; // 캐릭터가 주저앉는 현상 방지

        transform.position += offset;

        // 2. 카메라 바라보는 방향으로 캐릭터 회전
        transform.rotation = Quaternion.Euler(0f, mainCamera.eulerAngles.y, 0f);

        // 3. 손 위치와 회전 적용
        if (leftHandTarget && leftHandBone)
        {
            leftHandBone.position = leftHandTarget.position;
            leftHandBone.rotation = leftHandTarget.rotation * leftHandCorrection;
        }

        if (rightHandTarget && rightHandBone)
        {
            rightHandBone.position = rightHandTarget.position;
            rightHandBone.rotation = rightHandTarget.rotation * rightHandCorrection;
        }
    }
}
