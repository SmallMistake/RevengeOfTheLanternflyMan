using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is added to a revival totem that spawns when the player dies. It takes currency from the player when it spawns and only gives it back if the player collects it before dying again.
/// It self destucts if the player dies again before being collected.
/// </summary>
public class RevivalTotemController : MonoBehaviour, MMEventListener<TopDownEngineEvent>
{
    [SerializeField]
    private GameObject revivalTotemObject;

    /// <summary>
    /// Take currency from the player when the totem is spawned.
    /// </summary>
    void OnEnable()
    {
        //TODO Find the progress manager and take a percent of the players currency.
        TakeCurrencyFromPlayer();
        this.MMEventStartListening<TopDownEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<TopDownEngineEvent>();
    }

    /// <summary>
    /// If the player dies, destroy the totem.
    /// </summary>
    /// <param name="engineEvent"></param>
    public void OnMMEvent(TopDownEngineEvent engineEvent)
    {
        if (engineEvent.EventType == TopDownEngineEventTypes.PlayerDeath)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// If the player collects the totem, return the currency to the player.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character characterController = collision.gameObject.GetComponent<Character>();
            if (characterController != null && characterController.ConditionState.CurrentState != CharacterStates.CharacterConditions.Dead) {
                ReturnCurrencyToPlayer();
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Take currency from the player
    /// </summary>
    private void TakeCurrencyFromPlayer(){
        print("TODO Take Currency From Player using Progress Manager");
    }
    /// <summary>
    /// Return currency to the player
    /// </summary>
    private void ReturnCurrencyToPlayer()
    {
        print("TODO Return Currency To Player using Progress Manager");
    }
}
