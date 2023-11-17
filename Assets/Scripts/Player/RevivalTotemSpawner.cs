using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is added to a player to spawn revival totems when the player dies.
/// </summary>
public class RevivalTotemSpawner : MonoBehaviour, MMEventListener<TopDownEngineEvent>
{
    [SerializeField]
    [Tooltip("The prefab for the revival totem")]
    private GameObject revivalTotemObject;

    void OnEnable()
    {
        this.MMEventStartListening<TopDownEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<TopDownEngineEvent>();
    }

    /// <summary>
    /// When the player dies, spawn a revival totem.
    /// </summary>
    public void OnMMEvent(TopDownEngineEvent engineEvent)
    {
        if(engineEvent.EventType ==TopDownEngineEventTypes.PlayerDeath)
        {
            SpawnRevivalTotem();
        }
    }

    /// <summary>
    /// Spawn a revival totem at this objects location.
    /// </summary>
    public void SpawnRevivalTotem()
    {
        GameObject spawnedObject = Instantiate(revivalTotemObject);
        spawnedObject.transform.position = transform.position;
    }
}
