using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;  // Reference to the DialogueManager

    void Start()
    {
        if (dialogueManager != null)
        {
            // Trigger the dialogues that have already been set in the DialogueManager
            dialogueManager.StartNewDialogue(); // No parameters needed now
        }
        else
        {
            Debug.LogError("DialogueManager is not assigned!");
        }
    }
}
