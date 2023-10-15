using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedCharacterButtonActivation : CharacterButtonActivation
{
    /// if this is true, characters won't be able to jump while in a button activated zone
    [Tooltip("if this is true, characters won't be able to dash while in a button activated zone")]
    public bool PreventDashInButtonActivatedZone = true;
}
