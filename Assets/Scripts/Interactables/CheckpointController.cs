using MoreMountains.Feedbacks;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Summary <summary>
/// This script is used to change the floor of the object it is attached to.
///</summary>

public class CheckpointController : MonoBehaviour,
    MMEventListener<CheckPointEvent>
{
    [SerializeField]
    [Tooltip("Feedback called when Checkpoint Activated")]
    public MMF_Player checkpointActivatedFeedback;

    [SerializeField]
    [Tooltip("Feedback called when Checkpoint Deactivated")]
    public MMFeedbacks checkpointDeactivatedFeedback;

    [SerializeField]
    [Tooltip("Checkpoint script that is activated when this checkpoint is activated")]
    private CheckPoint checkpointToUse;

    protected void OnEnable()
    {
        this.MMEventStartListening<CheckPointEvent>();
    }

    protected void OnDisable()
    {
        this.MMEventStopListening<CheckPointEvent>();
    }


    /// <summary>
    /// Activated by an external force like a button activated area. When called it triggers a checkpoint event
    /// </summary>
    public void ActivateCheckpoint()
    {
        checkpointActivatedFeedback.PlayFeedbacks();
        LevelManager.Instance.SetCurrentCheckpoint(checkpointToUse);
        CheckPointEvent.Trigger(checkpointToUse.CheckPointOrder, checkpointToUse);
    }

    /// <summary>
    /// Grabs checkpoints events. If the checkpoint's order is > 0, we unlock our achievement
    /// </summary>
    /// <param name="checkPointEvent"></param>
    public virtual void OnMMEvent(CheckPointEvent checkPointEvent)
    {
        if (checkPointEvent.NewCheckpoint != checkpointToUse)
        {
            checkpointDeactivatedFeedback.PlayFeedbacks();
        }
    }
}
