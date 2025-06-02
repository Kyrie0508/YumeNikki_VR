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

    private Quaternion leftHandCorrection = Quaternion.Euler(0f, 270f, -90f);
    private Quaternion rightHandCorrection = Quaternion.Euler(0f, -270f, 90f);

    void Start()
    {
        // Rigidbody�� ������ ���� ���� ����
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        // 1. ī�޶�� ĳ���� �Ӹ� ���� ��� (Y�� ����)
        Vector3 offset = mainCamera.position - headBone.position;
        offset.y = 0f; // ĳ���Ͱ� �����ɴ� ���� ����

        transform.position += offset;

        // 2. ī�޶� �ٶ󺸴� �������� ĳ���� ȸ��
        transform.rotation = Quaternion.Euler(0f, mainCamera.eulerAngles.y, 0f);

        // 3. �� ��ġ�� ȸ�� ����
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
