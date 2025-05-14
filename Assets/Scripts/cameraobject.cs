using UnityEngine;

public class CapsuleFollower : MonoBehaviour
{
    public Transform cameraTransform;
    public bool canMove = true; // 이동 가능 여부

    void LateUpdate()
    {
        if (!canMove || cameraTransform == null) return;

        Vector3 pos = transform.position;
        pos.x = cameraTransform.position.x;
        pos.z = cameraTransform.position.z;
        transform.position = pos;
    }
}
