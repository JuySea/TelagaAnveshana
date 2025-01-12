using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;      // Reference to the dialogue panel (UI element)
    public TMP_Text characterNameText;    // Character name UI
    public TMP_Text dialogueText;         // Dialogue text UI
    public Image characterImage;          // Character sprite UI
    public float typingSpeed = 0.05f;     // Typing effect speed

    public Dialogue[] currentDialogues;   // Array of dialogues
    private int currentDialogueIndex = 0; // Tracks the current dialogue index
    private Coroutine typingCoroutine;    // Typing effect coroutine
    private bool isTyping = false;        // Is the system currently typing text?

    void Start()
    {
        // Optional: Initialize the dialogue panel to be inactive until needed
        dialoguePanel.SetActive(false);
    }

    // This method starts a new dialogue using the currentDialogues array set in the Inspector
    public void StartNewDialogue()
    {
        if (currentDialogues != null && currentDialogues.Length > 0)
        {
            // Start showing the first dialogue
            currentDialogueIndex = 0;
            ShowDialogue(currentDialogues[currentDialogueIndex]);

            // Show the dialogue panel
            dialoguePanel.SetActive(true);
        }
        else
        {
            Debug.LogError("No dialogues found in the DialogueManager!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            if (currentDialogueIndex < currentDialogues.Length - 1)
            {
                currentDialogueIndex++;
                ShowDialogue(currentDialogues[currentDialogueIndex]);
            }
            else
            {
                dialoguePanel.SetActive(false); // Hide dialogue panel when done
            }
        }
    }

    void ShowDialogue(Dialogue dialogue)
    {
        characterNameText.text = dialogue.characterName;
        characterImage.sprite = dialogue.characterSprite;
        characterImage.gameObject.SetActive(dialogue.characterSprite != null);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(dialogue.dialogueText));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
