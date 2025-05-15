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

    // 손목 회전 보정값
    private Quaternion leftHandCorrection = Quaternion.Euler(0f, 270f, -90f);
    private Quaternion rightHandCorrection = Quaternion.Euler(0f, -270f, 90f);

    void Update()
    {
        // 1. 캐릭터 이동
        Vector3 offset = mainCamera.position - headBone.position;
        transform.position += offset;

        // 2. 캐릭터 방향
        transform.rotation = Quaternion.Euler(0f, mainCamera.eulerAngles.y, 0f);

        // 3. 손 위치 및 회전 적용
        leftHandBone.position = leftHandTarget.position;
        leftHandBone.rotation = leftHandTarget.rotation * leftHandCorrection;

        rightHandBone.position = rightHandTarget.position;
        rightHandBone.rotation = rightHandTarget.rotation * rightHandCorrection;
    }
}
