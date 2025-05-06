using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingSpeed = 0.05f;

    private bool isTyping;
    private bool dialogueComplete;

    public void StartDialogue(string line)
    {
        StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        dialogueComplete = true;
    }

    private void Update()
    {
        
        if (dialogueComplete && (Input.GetKeyDown(KeyCode.G)))
        {
            Destroy(gameObject);
        }
    }
}