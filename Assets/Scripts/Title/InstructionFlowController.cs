using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionFlowController : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject warningPanel;
    public string nextSceneName = "WhiteDesert";

    private int clickCount = 0;

    private FadeTarget instructionFade;
    private FadeTarget warningFade;

    void Start()
    {
        instructionFade = instructionPanel.GetComponent<FadeTarget>();
        warningFade = warningPanel.GetComponent<FadeTarget>();
    }

    public void ResetFlow()
    {
        clickCount = 0;

        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
            instructionFade?.StartFadeIn();
        }

        if (warningPanel != null)
            warningPanel.SetActive(false);
    }

    public void OnTriggerClicked()
    {
        clickCount++;

        if (clickCount == 1)
        {
            instructionFade?.StartFadeOut();
            if (instructionPanel != null)
                instructionPanel.SetActive(false);

            if (warningPanel != null)
            {
                warningPanel.SetActive(true);
                warningFade?.StartFadeIn();
            }
        }
        else if (clickCount == 2)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}