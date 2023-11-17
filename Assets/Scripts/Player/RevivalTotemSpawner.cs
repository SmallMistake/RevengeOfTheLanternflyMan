using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is called when the player dies to store lost currency
public class RevivalTotemSpawner : MonoBehaviour, MMEventListener<TopDownEngineEvent>
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
        if(engineEvent.EventType ==TopDownEngineEventTypes.PlayerDeath)
        {
            SpawnRevivalTotem();
        }
    }
    public void SpawnRevivalTotem()
    {
        GameObject spawnedObject = Instantiate(revivalTotemObject);
        spawnedObject.transform.position =transform.position;
    }
}
