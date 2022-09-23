using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class DialogueTrigger : MonoBehaviour
{
    public Questline questline;
    public Dialogue unassignedDialogue;
    public Dialogue inProgressDialogue;
    public Dialogue completedDialogue;
    public Dialogue postCompletedDialogue;


    public void TriggerDialogue()
    {
        if(Time.timeScale != 0)
        {
            questline.CheckIfFinishedAllSubTasks();
            switch (questline.currentStatus)
            {
                case DialogueFlags.Unassigned:
                    FindObjectOfType<DialougeManager>().StartDialogue(unassignedDialogue, this);
                    break;
                case DialogueFlags.InProgress:
                    FindObjectOfType<DialougeManager>().StartDialogue(inProgressDialogue, this);
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
        switch (questline.currentStatus)
        {
            case DialogueFlags.Unassigned:
                questline.currentStatus = DialogueFlags.InProgress;
                break;
            case DialogueFlags.InProgress:
                break;
            case DialogueFlags.Completed:
                questline.currentStatus = DialogueFlags.PostCompleted;
                break;
        }
    }
}
