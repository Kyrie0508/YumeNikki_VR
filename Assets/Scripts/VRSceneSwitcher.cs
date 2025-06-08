using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VRSceneSwitcher : MonoBehaviour
{
    public InputActionProperty buttonAction;  // Input System의 버튼
    public string nextSceneName = "NextScene"; // 전환할 씬 이름

    private void OnEnable()
    {
        buttonAction.action.Enable();
    }

    private void OnDisable()
    {
        buttonAction.action.Disable();
    }

    private void Update()
    {
        if (buttonAction.action.WasPerformedThisFrame())
        {
            Debug.Log("버튼 눌림 - 씬 전환 시도");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
