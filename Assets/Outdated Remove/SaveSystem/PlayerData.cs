using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currency;
    public int level;
    public float timePlayed;
    public List<Utils.PermanentUpgrades> items;


    public PlayerData(int currency, int level, float timePlayed, List<Utils.PermanentUpgrades> items)
    {
        this.currency = currency;
        this.level = level;
        this.timePlayed = timePlayed;
       this.items = items;
    }
}
