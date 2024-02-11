using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this to allow easy to access bus functions. Used for things like stoping all music
public class FMODBusHelper : MonoBehaviour
{
    public string busName;
    FMOD.Studio.Bus bus;

    // Start is called before the first frame update
    void Start()
    {
        bus = FMODUnity.RuntimeManager.GetBus("Bus:/" + busName);
    }

    public void StopAudioFromBus(FMOD.Studio.STOP_MODE stopMode)
    {
        bus.stopAllEvents(stopMode);
    }

    public void ChangeBusVolumeLevel(float newVolumeLevel)
    {
        bus.setVolume(newVolumeLevel);
    }
}
