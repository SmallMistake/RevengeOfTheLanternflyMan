using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class DialogueTrigger : MonoBehaviour, Triggerable
{
    public string dialougeName = "NotNamed";
    public Questline questline;
    public Dialogue unassignedDialogue;
    public Dialogue inProgressDialogue;
    public Dialogue completedDialogue;
    public Dialogue postCompletedDialogue;

    public static event Action<string> DialougeEnded; //Used by Observers


    public void trigger()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        if(Time.timeScale != 0)
        {
            if(questline != null)
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
            else
            {
                FindObjectOfType<DialougeManager>().StartDialogue(unassignedDialogue, this);
            }
        }
    }

    public void HandleDialogueFinished()
    {
        if (questline != null)
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
        if(dialougeName != null && DialougeEnded != null)
        {
            DialougeEnded.Invoke(dialougeName);
        }
    }
}
