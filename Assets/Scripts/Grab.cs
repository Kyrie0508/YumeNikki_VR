using UnityEngine;

public class VRPickupObject : MonoBehaviour
{
    public Transform attachOffset;  // 오브젝트 내부 부착 기준점
    private bool isHeld = false;
    private Transform followTarget;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isHeld && followTarget != null)
        {
            Vector3 offset = transform.position - attachOffset.position;
            transform.position = followTarget.position + offset;
            transform.rotation = followTarget.rotation;
        }
    }

    public void Pickup(Transform handTarget)
    {
        isHeld = true;
        followTarget = handTarget;
        if (rb != null) rb.isKinematic = true;
    }

    public void Drop()
    {
        isHeld = false;
        followTarget = null;
        if (rb != null) rb.isKinematic = false;
    }

    public bool IsHeld() => isHeld;
}
