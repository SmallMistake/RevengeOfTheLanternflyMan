using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public TemperatureController temperatureController;
    public float burnPoint;

    private bool burning;

    public delegate void ChangedBurningState(bool burning);
    public ChangedBurningState burningStateChanged;

    private void OnEnable()
    {
        burningStateChanged?.Invoke(burning);
    }

    public void FixedUpdate()
    {
        if(temperatureController.currentTemperature >= burnPoint)
        {
            if (!burning)
            {
                StartBurning();
            }
        }
        else
        {
            if (burning)
            {
                StopBurning();
            }
        }
    }

    private void StartBurning()
    {
        burning = true;
        burningStateChanged?.Invoke(burning);
    }


    private void StopBurning()
    {
        burning = false;
        burningStateChanged?.Invoke(burning);
    }
}
