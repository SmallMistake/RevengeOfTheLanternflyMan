using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    internal enum Teams { Player, Enemy }

    public enum DialogueFlags { Unassigned, InProgress, Completed, PostCompleted}

    [System.Serializable]
    public enum PermanentUpgrades { Heart1, Heart2, Pecticide, VenusFlyTrap, Walnut}
}
