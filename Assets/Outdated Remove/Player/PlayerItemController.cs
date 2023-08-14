using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemController : MonoBehaviour
{
    public Utils.PermanentUpgrades itemName;

    public Utils.PermanentUpgrades GetCurrentItem()
    {
        return itemName;
    }
}
