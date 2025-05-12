using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionFlowController : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject warningPanel;
    public string nextSceneName = "WhiteDesert"; // 씬 이름에 맞게 변경 원래는 Basic Room인데 UI 테스트를 위해 임시로 정함

    private int clickCount = 0;

    public void ResetFlow()
    {
        clickCount = 0;

        if (instructionPanel != null)
            instructionPanel.SetActive(true);
        if (warningPanel != null)
            warningPanel.SetActive(false);
    }

    public void OnTriggerClicked()
    {
        clickCount++;

        if (clickCount == 1)
        {
            if (instructionPanel != null)
                instructionPanel.SetActive(false);

            if (warningPanel != null)
                warningPanel.SetActive(true);
        }
        else if (clickCount == 2)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}