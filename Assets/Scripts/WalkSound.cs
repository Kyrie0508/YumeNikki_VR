using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRAudioFootstep : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstepClip;
    public float stepInterval = 0.5f;

    public ActionBasedContinuousMoveProvider moveProvider;

    private float stepTimer = 0f;

    void Update()
    {
        Vector2 moveInput = Vector2.zero;

        if (moveProvider.leftHandMoveAction.action != null)
            moveInput = moveProvider.leftHandMoveAction.action.ReadValue<Vector2>();
        else if (moveProvider.rightHandMoveAction.action != null)
            moveInput = moveProvider.rightHandMoveAction.action.ReadValue<Vector2>();

        Debug.Log("Move input: " + moveInput);

        bool isMoving = moveInput.magnitude > 0.1f;

        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                Debug.Log("Footstep sound!");
                audioSource.PlayOneShot(footstepClip);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }
    }
}
