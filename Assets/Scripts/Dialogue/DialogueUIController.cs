using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class DialogueUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("효과음 설정")]
    [SerializeField] private AudioClip typingSFX;
    [SerializeField] private AudioSource audioSource;

    private bool isTyping;
    private bool dialogueComplete;

    private string[] dialogueLines;
    private int currentIndex = 0;

    private InputDevice leftController;
    private InputDevice rightController;
    private bool wasPressedLastFrame = false;

    private void Start()
    {
        // XR 컨트롤러 초기화
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        if (devices.Count > 0)
            leftController = devices[0];

        devices.Clear();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)
            rightController = devices[0];
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentIndex = 0;
        StartCoroutine(TypeLine(dialogueLines[currentIndex]));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueComplete = false;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;

            // 효과음 재생 (공백 문자 제외)
            if (typingSFX != null && audioSource != null && !char.IsWhiteSpace(c))
            {
                audioSource.PlayOneShot(typingSFX);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        dialogueComplete = true;
    }

    private void Update()
    {
        if (!leftController.isValid || !rightController.isValid)
            return;

        bool isGripPressed = false;

        if (leftController.TryGetFeatureValue(CommonUsages.gripButton, out bool leftPressed) && leftPressed)
            isGripPressed = true;

        if (rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool rightPressed) && rightPressed)
            isGripPressed = true;

        if (isGripPressed && !wasPressedLastFrame && dialogueComplete && !isTyping)
        {
            currentIndex++;

            if (currentIndex < dialogueLines.Length)
            {
                StartCoroutine(TypeLine(dialogueLines[currentIndex]));
            }
            else
            {
                Destroy(gameObject);
            }
        }

        wasPressedLastFrame = isGripPressed;
    }
}
