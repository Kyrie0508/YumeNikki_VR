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
        instructionCanvas?.SetActive(true);
        instructionPanel?.SetActive(true);
        instructionFlowController?.ResetFlow();
    }
    
    public void OnExitClicked()
    {
        Application.Quit();
    }
}