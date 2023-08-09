using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestLineData {

    public List<Questline> questlines;

    public void AddQuestline(Questline newQuestline)
    {
        if (checkIfQuestlineIsNew(newQuestline))
        {
            Debug.Log("Accepted new quest: " + newQuestline.QuestName);
        }
        else
        {
            Debug.Log("Already Accepted Questline: " + newQuestline.QuestName);
        }

    }

    private bool checkIfQuestlineIsNew(Questline newQuestline)
    {
        bool notAcceptedBefore = true;
        foreach (Questline questline in questlines)
        {
            if (questline.QuestName.Equals(newQuestline.QuestName))
            {
                notAcceptedBefore = false;
                break;
            }
        }
        return notAcceptedBefore;
    }
}
