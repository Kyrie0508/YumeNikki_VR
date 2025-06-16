using System;
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
    private bool dialogueStarted = false;
    
    private InputDevice leftController;
    private InputDevice rightController;
    private bool wasPressedLastFrame = false;

    public event Action OnDialogueComplete;

    private void Start()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        if (devices.Count > 0) leftController = devices[0];

        devices.Clear();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0) rightController = devices[0];
    }

    public void StartDialogue(string[] lines)
    {
        if (dialogueStarted) return; // 이미 시작했으면 무시

        dialogueStarted = true;
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

            if (typingSFX != null && audioSource != null && !char.IsWhiteSpace(c))
                audioSource.PlayOneShot(typingSFX);

            yield return new WaitForSeconds(typingSpeed);
        }

        if (audioSource != null)
            audioSource.Stop();

        isTyping = false;
        dialogueComplete = true;
    }

    private void Update()
    {
        if (!leftController.isValid || !rightController.isValid) return;

        bool isGripPressed =
            (leftController.TryGetFeatureValue(CommonUsages.gripButton, out bool left) && left) ||
            (rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool right) && right);

        if (isGripPressed && !wasPressedLastFrame)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentIndex];
                isTyping = false;
                dialogueComplete = true;
            }
            else if (dialogueComplete)
            {
                currentIndex++;

                if (currentIndex < dialogueLines.Length)
                {
                    StartCoroutine(TypeLine(dialogueLines[currentIndex]));
                }
                else
                {
                    OnDialogueComplete?.Invoke();
                    Destroy(gameObject); // 무조건 삭제
                }
            }
        }

        wasPressedLastFrame = isGripPressed;
    }
}
