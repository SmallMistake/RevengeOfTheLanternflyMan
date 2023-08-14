using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Questlines/SubObjective", order = 2)]
[System.Serializable]
public class SubObjective : ScriptableObject
{
    public string subObjectiveName;
    public bool status;

    internal Questline questline;

    private void OnEnable()
    {
        ISubObjectiveTrigger.SubObjectiveChanged += SubObjectiveCriteriaChanged;
    }


    public void SubObjectiveCriteriaChanged(string targetedSubobjective, bool newStatus)
    {
        if (subObjectiveName.Equals(targetedSubobjective))
        {
            status = newStatus;
            questline.CheckIfFinishedAllSubTasks();
        }
    }
}
