using UnityEngine;

public class VRHandAutoGrab : MonoBehaviour
{
    public Transform handTarget; // IK 손 위치 (예: rightHandTarget)

    private void OnTriggerEnter(Collider other)
    {
        var pickup = other.GetComponent<VRPickupObject>();
        if (pickup != null && !pickup.IsHeld())
        {
            pickup.Pickup(handTarget);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var pickup = other.GetComponent<VRPickupObject>();
        if (pickup != null && pickup.IsHeld())
        {
            pickup.Drop();
        }
    }
}
