using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerData : MonoBehaviour
{
    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }
}
