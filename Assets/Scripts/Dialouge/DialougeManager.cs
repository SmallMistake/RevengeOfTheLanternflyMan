using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeManager : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMeshProUGUI nameUI;
    public TextMeshProUGUI dialogueUI;
    public Animator animator;
    private bool dialogueActive = false;
    private bool closedThisFrame = false;

    private DialogueTrigger dialogueTrigger;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Primary") && dialogueActive)
        {
            DisplayNextSentence();
        }
    }
    private void LateUpdate()
    {
        closedThisFrame = false;
    }

    public void StartDialogue(Dialogue dialouge, DialogueTrigger trigger)
    {
        if (!closedThisFrame)
        {
            dialogueTrigger = trigger;
            Time.timeScale = 0;
            dialogueActive = true;
            nameUI.text = dialouge.speakerName;
            animator.SetTrigger("OpenDialogue");

            sentences.Clear();
            dialogueUI.text = "";

            foreach (string sentence in dialouge.sentences)
            {
                sentences.Enqueue(sentence);
            }
            StartCoroutine(WaitWhilePanelRises());
        }
    }

    IEnumerator WaitWhilePanelRises()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueUI.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueUI.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetTrigger("CloseDialogue");
        dialogueTrigger.HandleDialogueFinished();
        dialogueTrigger = null;
        dialogueActive = false;
        Time.timeScale = 1;
        closedThisFrame = true;
    }
}
