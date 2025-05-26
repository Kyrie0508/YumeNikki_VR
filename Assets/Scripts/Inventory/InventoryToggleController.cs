using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryToggleController : MonoBehaviour
{
    [Header("Inventory UI")]
    public GameObject inventoryUI; // Canvas_InventoryUI
    public Transform cameraTransform;
    public Vector3 offsetFromCamera = new Vector3(0, -0.2f, 2.0f);
    public float uiScale = 0.0025f;

    [Header("XR Input Action")]
    public InputActionProperty toggleAction; // 오른손 A버튼, 왼손 X버튼 등

    private bool isVisible = false;

    private void OnEnable()
    {
        toggleAction.action.Enable();
        toggleAction.action.performed += OnTogglePressed;
    }

    private void OnDisable()
    {
        toggleAction.action.performed -= OnTogglePressed;
        toggleAction.action.Disable();
    }

    private void OnTogglePressed(InputAction.CallbackContext ctx)
    {
        isVisible = !isVisible;
        inventoryUI.SetActive(isVisible);

        if (isVisible)
        {
            // 카메라 앞에 UI 배치
            inventoryUI.transform.position = cameraTransform.position + cameraTransform.forward * offsetFromCamera.z + cameraTransform.up * offsetFromCamera.y;
            inventoryUI.transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
            inventoryUI.transform.localScale = Vector3.one * uiScale;
        }
    }
}