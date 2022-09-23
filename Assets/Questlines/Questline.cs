using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Questlines/Questline", order = 1)]
[System.Serializable]
public class Questline : ScriptableObject
{
    public string QuestName;
    public List<SubObjective> subObjectives;
    public Utils.DialogueFlags currentStatus;

    private void OnEnable()
    {
        foreach (SubObjective subObjective in subObjectives)
        {
            subObjective.questline = this;
        }
        CheckIfFinishedAllSubTasks();
    }


    internal bool CheckIfFinishedAllSubTasks()
    {
        bool finished = true;

        foreach (SubObjective subObjective in subObjectives)
        {
            if(subObjective.status == false)
            {
                finished = false;
                break;
            }
        }

        if (finished && currentStatus.Equals(Utils.DialogueFlags.InProgress))
        {
            currentStatus = Utils.DialogueFlags.Completed;
        }

        return finished;
    }
}