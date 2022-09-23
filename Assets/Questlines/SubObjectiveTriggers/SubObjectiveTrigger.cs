using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISubObjectiveTrigger: MonoBehaviour
{
    public string subObjectiveName;
    public static Action<string, bool> SubObjectiveChanged;
}
