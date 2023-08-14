using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureController : MonoBehaviour
{
    public float currentTemperature;

    public float maxTemperature;
    public float minTemperature;

    public delegate void TemperatureChanged(float changeAmount);
    public TemperatureChanged temperatureChanged;

    public void ChangeTemperature(float changeAmount, float? minAmount = null)
    {
        currentTemperature += changeAmount;
        if(minAmount != null && currentTemperature < minAmount)
        {
            currentTemperature = (float)minAmount;
        }
        temperatureChanged?.Invoke(currentTemperature);
        CheckIfTempIsValid();
    }  

    public void CheckIfTempIsValid()
    {
        if (currentTemperature > maxTemperature)
        {
            currentTemperature = maxTemperature;
        }
        else if (currentTemperature < minTemperature)
        {
            currentTemperature = minTemperature;
        }
    }
}
