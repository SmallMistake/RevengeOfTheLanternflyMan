using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSetPlayerData : MonoBehaviour
{
    public List<Utils.PermanentUpgrades> upgradesToGivePlayer;
    // Start is called before the first frame update
    void Start()
    {
        if(upgradesToGivePlayer != null)
        {
            InventoryController playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
            foreach (Utils.PermanentUpgrades upgrade in upgradesToGivePlayer)
            {
                playerInventory.UnlockedItem(upgrade);
            }
        }
    }
}
