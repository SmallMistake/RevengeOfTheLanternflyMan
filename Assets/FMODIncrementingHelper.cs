using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODIncrementingHelper : MonoBehaviour
{
    //TODO expand this to be used for different events
    private static int currentPickupCombo = 0;
    private float comboWaitTime = 3f;
    static FMOD.Studio.EventInstance PickupEvent;
    private Coroutine cooldown;
    // Start is called before the first frame update
    void Start()
    {
        PickupEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Pickup");
    }

    public void handleIncrement()
    {
        currentPickupCombo++;
        PickupEvent.setParameterByName("Combo", currentPickupCombo);
        PickupEvent.start();
        if (cooldown != null)
        {
            StopCoroutine(cooldown);
        }
        cooldown = StartCoroutine(pickupCooldown());
    }

    IEnumerator pickupCooldown()
    {
        print("Started");
        yield return new WaitForSeconds(comboWaitTime);

        currentPickupCombo = 0;
        print("Reset");
        PickupEvent.setParameterByName("Combo", currentPickupCombo);
    }

    static private void OnDisable()
    {
        currentPickupCombo = 0;
        PickupEvent.setParameterByName("Combo", currentPickupCombo);
    }
}
