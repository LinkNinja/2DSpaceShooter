using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDialogueSystem : MonoBehaviour
{
    public TMP_Text dialogueText;
    public List<CharacterDialogueData> characters;
    public AudioClip typingSound;
    public float typingSpeed = 0.02f;
    private AudioSource audioSource;

    [System.Serializable]
    public class DialogueLine
    {
        public string text;
        public int characterIndex;
        public string animationState;
    }

    [System.Serializable]
    public class CharacterDialogueData
    {
        public GameObject portraitObject;
    }

    public List<DialogueLine> startDialogueLines;  // Store initial dialogue lines
    public List<DialogueLine> bossDialogueLines;   // Store boss dialogue lines

    // Add this line to declare the dialogueLines list
    private List<DialogueLine> dialogueLines;

    private int currentLineIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (var character in characters)
        {
            character.portraitObject.SetActive(false);
        }
    }

    public void StartDialogue(List<DialogueLine> lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Count)
        {
            var currentLine = dialogueLines[currentLineIndex];
            StartCoroutine(TypeDialogue(currentLine.text, currentLine.characterIndex, currentLine.animationState));
            currentLineIndex++;
        }
        else
        {
            dialogueText.text = "";
            Time.timeScale = 1f;
        }
    }

    IEnumerator TypeDialogue(string dialogue, int characterIndex, string animationState)
    {
        characters[characterIndex].portraitObject.SetActive(true);
        Animator animator = characters[characterIndex].portraitObject.GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.Play(animationState);
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            audioSource.PlayOneShot(typingSound);
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        yield return new WaitForSecondsRealtime(2f);
        characters[characterIndex].portraitObject.SetActive(false);
        DisplayNextLine();
    }

    public void TriggerDialogueAtStart()
    {
        Time.timeScale = 0f;
        StartDialogue(startDialogueLines);
    }

    public void TriggerDialogueAtBoss()
    {
        Time.timeScale = 0f;
        StartDialogue(bossDialogueLines);
    }
}
