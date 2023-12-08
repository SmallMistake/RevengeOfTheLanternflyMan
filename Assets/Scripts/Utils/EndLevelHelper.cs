using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelHelper : MonoBehaviour
{
    public void EndLevel()
    {
        Character character = LevelManager.Instance.Players[0];
        TopDownEngineEvent.Trigger(TopDownEngineEventTypes.LevelComplete, character);
    }
}
