using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatEmitter : MonoBehaviour
{
    public bool emitting;
    public float heatEmitAmount;

    private List<TemperatureController> temperatureControllersInRange = new List<TemperatureController>();

    public void StartEmitting()
    {
        emitting = true;
    }

    public void StopEmitting()
    {
        emitting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TemperatureController temperatureController = collision.GetComponent<TemperatureController>();
        if (temperatureController != null)
        {
            if (!temperatureControllersInRange.Contains(temperatureController))
            {
                temperatureControllersInRange.Add(temperatureController);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TemperatureController temperatureController = collision.GetComponent<TemperatureController>();
        if (temperatureController != null)
        {
            if (temperatureControllersInRange.Contains(temperatureController))
            {
                temperatureControllersInRange.Remove(temperatureController);
            }
        }
    }

    public void FixedUpdate()
    {
        if (emitting)
        {
            OutputHeat();
        }
    }

    private void OutputHeat()
    {
        foreach (TemperatureController temperatureController in temperatureControllersInRange)
        {
            temperatureController.ChangeTemperature(heatEmitAmount);
        }
    }
}
