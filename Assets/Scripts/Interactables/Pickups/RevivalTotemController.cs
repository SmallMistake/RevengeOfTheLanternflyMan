using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalTotemController : MonoBehaviour, MMEventListener<TopDownEngineEvent>
{
    [SerializeField]
    private GameObject revivalTotemObject;

    void OnEnable()
    {
        this.MMEventStartListening<TopDownEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<TopDownEngineEvent>();
    }

    public void OnMMEvent(TopDownEngineEvent engineEvent)
    {
        if (engineEvent.EventType == TopDownEngineEventTypes.PlayerDeath)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character characterController = collision.gameObject.GetComponent<Character>();
            if (characterController != null && characterController.ConditionState.CurrentState != CharacterStates.CharacterConditions.Dead) {
                print("TODO Regive Lost Items");
                Destroy(gameObject);
            }
        }
    }
}
