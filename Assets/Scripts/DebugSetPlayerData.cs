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
            PlayerInventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
            foreach (Utils.PermanentUpgrades upgrade in upgradesToGivePlayer)
            {
                playerInventory.UnlockedItem(upgrade);
            }
        }
    }
}
