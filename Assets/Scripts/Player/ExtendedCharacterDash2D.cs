using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedCharacterDash2D : CharacterDash2D
{

    protected const string _dodgeRollingObjectAnimationParameterName = "DodgeRolling";
    protected int _dodgeRollingObjectAnimationParameter;

    /// <summary>
    /// Overriden
    /// </summary>
    protected override void HandleInput()
    {
        //Overriden by Try
    }

    /// <summary>
    /// If called try to dash
    /// </summary>
    public override bool IsAbilityActivatable()
    {
        return true;
    }

    public override void Activate()
    {
        base.HandleInput();
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
