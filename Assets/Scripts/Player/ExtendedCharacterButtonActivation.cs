using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedCharacterButtonActivation : CharacterButtonActivation
{
    /// if this is true, characters won't be able to jump while in a button activated zone
    [Tooltip("if this is true, characters won't be able to dash while in a button activated zone")]
    public bool PreventDashInButtonActivatedZone = true;

    protected override void HandleInput()
    {
        //Overriden
    }

    /// <summary>
    /// Try to interact with objects
    /// </summary>
    public override bool IsAbilityActivatable()
    {
        if (!AbilityAuthorized)
        {
            return false;
        }
        if (InButtonActivatedZone && (ButtonActivatedZone != null))
        {
            return true;
        }
        return false;
    }

    public override void Activate()
    {
        bool buttonPressed = false;
        switch (ButtonActivatedZone.InputType)
        {
            case ButtonActivated.InputTypes.Default:
                buttonPressed = (_inputManager.InteractButton.State.CurrentState == MMInput.ButtonStates.ButtonDown);
                break;
        #if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
						case ButtonActivated.InputTypes.Button:
						case ButtonActivated.InputTypes.Key:
							buttonPressed = ButtonActivatedZone.InputActionPerformed;
							break;
        #else
            case ButtonActivated.InputTypes.Button:
                buttonPressed = (Input.GetButtonDown(_character.PlayerID + "_" + ButtonActivatedZone.InputButton));
                break;
            case ButtonActivated.InputTypes.Key:
                buttonPressed = (Input.GetKeyDown(ButtonActivatedZone.InputKey));
                break;
        #endif
        }

        if (buttonPressed)
        {
            ButtonActivation();
        }
    }
}
