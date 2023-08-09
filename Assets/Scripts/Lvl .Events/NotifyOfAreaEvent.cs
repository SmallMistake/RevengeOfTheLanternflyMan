using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotifyOfAreaController : MonoBehaviour
{
    public string areaName;
    
    public void NotifyOfArea()
    {
        GameObject ui = (GameObject)Instantiate(Resources.Load("UI/RoomNotificationUI"));
        ui.GetComponentInChildren<TextMeshProUGUI>().text = areaName;
    }
}
