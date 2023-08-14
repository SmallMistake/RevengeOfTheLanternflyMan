using System;
using UnityEngine;

public class PauseInterrupter : MonoBehaviour
{
    public static event Action<bool> ChangeCanPauseStatus;

    public void ChangePausableStatus(int canPause) // 0 = false 1 = true
    {
        if(ChangeCanPauseStatus != null)
        {
            if (canPause == 1)
            {
                ChangeCanPauseStatus.Invoke(true);
            }
            else if (canPause == 0)
            {
                ChangeCanPauseStatus.Invoke(false);
            }
            else if (canPause != 0 || canPause != 1)
            {
                throw new ArgumentException("Parameter must be either 1 or 0", nameof(canPause));
            }
        }
    }
}
