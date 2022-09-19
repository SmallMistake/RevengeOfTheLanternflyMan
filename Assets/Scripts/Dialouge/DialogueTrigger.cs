using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueFlags currentStageOfDialogue;
    public Dialogue unassignedDialogue;
    public Dialogue startedDialogue;
    public Dialogue completedDialogue;
    public Dialogue postCompletedDialogue;


    public void TriggerDialogue()
    {
        if(Time.timeScale != 0)
        {
            switch (currentStageOfDialogue)
            {
                case DialogueFlags.Unassigned:
                    FindObjectOfType<DialougeManager>().StartDialogue(unassignedDialogue, this);
                    break;
                case DialogueFlags.Started:
                    FindObjectOfType<DialougeManager>().StartDialogue(startedDialogue, this);
                    break;
                case DialogueFlags.Completed:
                    FindObjectOfType<DialougeManager>().StartDialogue(completedDialogue, this);
                    break;
                case DialogueFlags.PostCompleted:
                    FindObjectOfType<DialougeManager>().StartDialogue(postCompletedDialogue, this);
                    break;
            }
        }
    }

    public void HandleDialogueFinished()
    {
        switch (currentStageOfDialogue)
        {
            case DialogueFlags.Unassigned:
                currentStageOfDialogue = DialogueFlags.Started;
                break;
            case DialogueFlags.Started:
                currentStageOfDialogue = DialogueFlags.Completed;
                break;
            case DialogueFlags.Completed:
                currentStageOfDialogue = DialogueFlags.PostCompleted;
                break;
        }
        print("TODO Dialogue Finished");
    }
}
