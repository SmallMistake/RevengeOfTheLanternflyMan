using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedCharacterDash2D : CharacterDash2D
{
    protected ExtendedCharacterButtonActivation _characterButtonActivation;


    /// <summary>
    /// On init we grab other components
    /// </summary>
    protected override void Initialization()
    {
        base.Initialization();
        _characterButtonActivation = _character?.FindAbility<ExtendedCharacterButtonActivation>();
    }

    /// <summary>
    /// Watches for dash inputs
    /// </summary>
    protected override void HandleInput()
    {
        if (_characterButtonActivation == null || (_characterButtonActivation.PreventDashInButtonActivatedZone && !_characterButtonActivation.InButtonActivatedZone))
        {
            base.HandleInput();
        }
    }
}
