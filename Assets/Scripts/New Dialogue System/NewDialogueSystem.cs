using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDialogueSystem : MonoBehaviour
{
    public TMP_Text dialogueText;
    public List<CharacterDialogueData> characters;
    //public AudioClip typingSound;
    public float typingSpeed = 0.02f;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    public GameObject spaceBar;
    public GameObject controls;
    public GameManager gameManager;

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

    // Store initial dialogue lines
    public List<DialogueLine> startDialogueLines;  

    // Store boss dialogue lines
    public List<DialogueLine> bossDialogueLines;   

    private List<DialogueLine> dialogueLines;
    private int currentLineIndex = 0;

    void Start()
    {
        HideAllPortraits();
        controls.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Skip typing animation and display full dialogue text
                StopCoroutine(typingCoroutine);
                DisplayFullCurrentLine();
            }
            else
            {
                DisplayNextLine();
            }
        }
    }

    public void StartDialogue(List<DialogueLine> lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        controls.SetActive(false);
        spaceBar.SetActive(true);

        // Notify GameManager to switch to Dialogue state
        gameManager.StartDialogue();

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Count)
        {
            var currentLine = dialogueLines[currentLineIndex];
            typingCoroutine = StartCoroutine(TypeDialogue(currentLine.text, currentLine.characterIndex, currentLine.animationState));
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeDialogue(string dialogue, int characterIndex, string animationState)
    {
        // Check it's not the first line
        if (currentLineIndex > 1) 
        {
            characters[dialogueLines[currentLineIndex - 2].characterIndex].portraitObject.SetActive(false);
        }

        characters[characterIndex].portraitObject.SetActive(true);
        Animator animator = characters[characterIndex].portraitObject.GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.Play(animationState);
        dialogueText.text = "";
        isTyping = true;
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            //FindObjectOfType<AudioManager>().Play("DialogueSound");
            AudioManager.Instance.Play("DialogueSound");
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        isTyping = false;
        yield return new WaitForSecondsRealtime(2f);
        DisplayNextLine();
    }

    void DisplayFullCurrentLine()
    {
        var currentLine = dialogueLines[currentLineIndex - 1];
        dialogueText.text = currentLine.text;
        //FindObjectOfType<AudioManager>().Play("DialogueSound");
        AudioManager.Instance.Play("DialogueSound");
        isTyping = false;
        StopAllCoroutines();
    }

    void HideAllPortraits()
    {
        foreach (var character in characters)
        {
            character.portraitObject.SetActive(false);
        }
    }

    public void TriggerDialogueAtStart()
    {
        StartDialogue(startDialogueLines);
    }

    public void TriggerDialogueAtBoss()
    {
        StartDialogue(bossDialogueLines);
    }

    private void EndDialogue()
    {
        dialogueText.text = "";
        // Show Controls
        controls.SetActive(true);
        // Hide Spacebar key
        spaceBar.SetActive(false);
        // Hide portraits once the dialogue is finished
        HideAllPortraits();

        // Notify GameManager to switch back to Playing state
        gameManager.EndDialogue();
    }
}
