using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedCharacterDash2D : CharacterDash2D
{
    protected ExtendedCharacterButtonActivation _characterButtonActivation;


    protected const string _dodgeRollingObjectAnimationParameterName = "DodgeRolling";
    protected int _dodgeRollingObjectAnimationParameter;

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

    protected override void InitializeAnimatorParameters()
    {
        RegisterAnimatorParameter(_dodgeRollingObjectAnimationParameterName, AnimatorControllerParameterType.Bool, out _dodgeRollingObjectAnimationParameter);
    }

    /// <summary>
    /// Sends the current speed and the current value of the Walking state to the animator
    /// </summary>
    public override void UpdateAnimator()
    {
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, _dodgeRollingObjectAnimationParameter, (_movement.CurrentState == CharacterStates.MovementStates.Dashing), _character._animatorParameters, _character.RunAnimatorSanityChecks);
    }
}
