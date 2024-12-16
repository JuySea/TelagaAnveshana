using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string characterName;   // Name of the character
    [TextArea(3, 10)]
    public string dialogueText;    // Dialogue text
    public Sprite characterSprite; // Optional character sprite
}
