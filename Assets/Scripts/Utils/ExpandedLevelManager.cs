using IntronDigital;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandedLevelManager : LevelManager, MMEventListener<TransitionEvent>,
    MMEventListener<TopDownEngineEvent>
{
    [SerializeField]
    [Tooltip("Used to respawn in level")]
    LevelSelector levelSelector;

    [SerializeField]
    string sceneToLoadOnLevelCompletion;

    protected override IEnumerator PlayerDeadCo()
    {
        yield return new WaitForSeconds(DelayBeforeDeathScreen);
        TransitionEvent.Trigger(TransitionEventTypes.ToBlack, "Circle"); //TODO this can be expanded to be more variable
        this.MMEventStartListening<TransitionEvent>();
        //GUIManager.Instance.SetDeathScreen(true); //THis is being replace by fade out and back in
    }

    // Listen to transition events.
    public void OnMMEvent(TransitionEvent eventType)
    {
        switch(eventType.EventType) {
            //When Played out play back in and respawn level.
            case TransitionEventTypes.FinishedToBlack:
                this.MMEventStopListening<TransitionEvent>();
                this.MMEventStartListening<TopDownEngineEvent>();
                levelSelector.RestartLevel();
                break;
        }
    }

    // On Level Respawn Handle
    public override void OnMMEvent(TopDownEngineEvent eventType)
    {
        switch (eventType.EventType)
        {
            //When Played out play back in and respawn level.
            case TopDownEngineEventTypes.RespawnComplete:
                TransitionEvent.Trigger(TransitionEventTypes.OutOfBlack, "Circle");
                this.MMEventStopListening<TopDownEngineEvent>();
                break;
            case TopDownEngineEventTypes.LevelComplete:
                StartCoroutine(GotoLevelCo(sceneToLoadOnLevelCompletion));
                break;
        }
        base.OnMMEvent(eventType);
    }
}
