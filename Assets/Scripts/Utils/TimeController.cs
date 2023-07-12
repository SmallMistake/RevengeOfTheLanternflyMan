using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public void SetTimeScaleTo(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
