using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem{ 

    public static void SavePlayer(int currency, int level, float timePlayed, List<Utils.PermanentUpgrades> items)
    {
        SaveToPlayerPrefs(currency, level, timePlayed, items);
    }

    public static PlayerData LoadPlayer()
    {
        return LoadFromPlayerPrefs();
    }

    private static void SaveToPlayerPrefs(int currency, int level, float timePlayed, List<Utils.PermanentUpgrades> items)
    {
        PlayerPrefs.SetInt("Walnuts", currency);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetFloat("TimePlayed", timePlayed);
        string upgradeString = "";
        foreach (Utils.PermanentUpgrades item in items)
        {
            upgradeString += item + ",";
        }
        PlayerPrefs.SetString("Upgrades", upgradeString);

        PlayerPrefs.Save();
    }

    //Not In Use Right now
    private static void SaveToFile(int currency, int level, float timePlayed, List<Utils.PermanentUpgrades> items)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(currency, level, timePlayed, items);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    private static PlayerData LoadFromPlayerPrefs()
    {
        int walnuts = PlayerPrefs.GetInt("Walnuts");
        int level = PlayerPrefs.GetInt("Level");
        float timePlayer = PlayerPrefs.GetFloat("TimePlayed");
        string upgradesString = PlayerPrefs.GetString("Upgrades");

        if (upgradesString == null)
        {
            SaveToPlayerPrefs(0, 0, 0, new List<Utils.PermanentUpgrades>());
            upgradesString = "";
        }

        List<Utils.PermanentUpgrades> upgrades = new List<Utils.PermanentUpgrades>();
        foreach (string upgrade in upgradesString.Split(","))
        {
            switch(upgrade) {
                case "VenusFlyTrap":
                    upgrades.Add(Utils.PermanentUpgrades.VenusFlyTrap);
                    break;
                case "Walnut":
                    upgrades.Add(Utils.PermanentUpgrades.Walnut);
                    break;
                case "Pecticide":
                    upgrades.Add(Utils.PermanentUpgrades.Pecticide);
                    break;
            }
        }


        PlayerData data = new PlayerData(
            PlayerPrefs.GetInt("Walnuts"),
            PlayerPrefs.GetInt("Level"),
            PlayerPrefs.GetFloat("TimePlayed"),
            upgrades
        );

        return data;
    }

    //Not In Use Right now
    private static PlayerData LoadFromFile()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            SavePlayer(0, -99, 0, new List<Utils.PermanentUpgrades>());
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
