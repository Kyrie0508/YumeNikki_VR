using UnityEngine;

public class TitleButtonController : MonoBehaviour
{
    public GameObject instructionCanvas;
    public GameObject instructionPanel;
    public GameObject canvas;
    public GameObject warningPanel;
    public InstructionFlowController instructionFlowController;

    void Start()
    {
        if (instructionCanvas != null)
            instructionCanvas.SetActive(false);
    }

    public void OnNewGameClicked()
    {
        canvas.SetActive(false);
        if (instructionCanvas != null)
            instructionCanvas.SetActive(true);

        if (instructionPanel != null)
            instructionPanel.SetActive(true);

        if (instructionFlowController != null)
        {
           
            instructionFlowController.ResetFlow();
        }
        
    }

    public void OnDreamClicked()
    {
        Debug.Log("Dream button clicked (미구현)");
        // TODO: 나중에 구현
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}