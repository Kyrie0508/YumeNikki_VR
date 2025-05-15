using UnityEngine;

public class VRAvatarFollow : MonoBehaviour
{
    public Transform xrOrigin;             // XR Rig
    public Transform mainCamera;           // Main Camera
    public Transform headBone;             // ĳ������ Head
    public Transform leftHandTarget;       // �޼� ��Ʈ�ѷ�
    public Transform rightHandTarget;      // ������ ��Ʈ�ѷ�
    public Transform leftHandBone;         // ĳ���� �޼�
    public Transform rightHandBone;        // ĳ���� ������

    // �ո� ȸ�� ������
    private Quaternion leftHandCorrection = Quaternion.Euler(0f, 270f, -90f);
    private Quaternion rightHandCorrection = Quaternion.Euler(0f, -270f, 90f);

    void Update()
    {
        // 1. ĳ���� �̵�
        Vector3 offset = mainCamera.position - headBone.position;
        transform.position += offset;

        // 2. ĳ���� ����
        transform.rotation = Quaternion.Euler(0f, mainCamera.eulerAngles.y, 0f);

        // 3. �� ��ġ �� ȸ�� ����
        leftHandBone.position = leftHandTarget.position;
        leftHandBone.rotation = leftHandTarget.rotation * leftHandCorrection;

        rightHandBone.position = rightHandTarget.position;
        rightHandBone.rotation = rightHandTarget.rotation * rightHandCorrection;
    }
}
