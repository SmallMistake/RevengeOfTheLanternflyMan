using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour,
    MMEventListener<CheckPointEvent>
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CheckPoint checkpointToUse;
    
    private bool activated = false;

    protected void OnEnable()
    {
        this.MMEventStartListening<CheckPointEvent>();
    }

    protected void OnDisable()
    {
        this.MMEventStopListening<CheckPointEvent>();
    }


    public void ActivateCheckpoint()
    {
        animator.SetTrigger("Activated");
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
            animator.SetTrigger("Deactivated");
        }
    }
}
