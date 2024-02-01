using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
    public void KillPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            Character character = p.GetComponent<Character>();
            if(character != null)
            {
                if(character.PlayerID == "Player1")
                {
                    p.GetComponent<Health>().Kill();
                }
            }
        }
    }

    public void FinishLevel()
    {
        Character character = LevelManager.Instance.Players[0];
        TopDownEngineEvent.Trigger(TopDownEngineEventTypes.LevelComplete, character);
    }
}
