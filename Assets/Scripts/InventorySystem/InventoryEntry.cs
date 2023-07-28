using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class InventoryEntry
{
    public string Name;
    public string Description;
    public int amountHeld;

    // method for cloning object
    public InventoryEntry ShallowCopy()
    {
        return (InventoryEntry)MemberwiseClone();
    }
}
