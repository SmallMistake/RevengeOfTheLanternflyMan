using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCharacterFallDownHoles2D : CharacterFallDownHoles2D
{
    public float damageAmount = 2;
    public float flickerDuration = 1;
    public float invincibilityDuration = 1;
    /// <summary>
    /// if we find a hole below our character, we kill our character
    /// </summary>
    protected override void CheckForHoles()
    {
        if (!AbilityAuthorized)
        {
            return;
        }

        if (_character.ConditionState.CurrentState == CharacterStates.CharacterConditions.Dead)
        {
            return;
        }

        if (_controller2D.OverHole && !_controller2D.Grounded && _movement.CurrentState != CharacterStates.MovementStates.FallingDownHole)
        {
            if ((_movement.CurrentState != CharacterStates.MovementStates.Jumping)
                && (_movement.CurrentState != CharacterStates.MovementStates.Dashing)
                && (_condition.CurrentState != CharacterStates.CharacterConditions.Dead))
            {
                _movement.ChangeState(CharacterStates.MovementStates.FallingDownHole);
                FallingFeedback?.PlayFeedbacks(this.transform.position);
                PlayAbilityStartFeedbacks();
                DamagePlayer();
            }
        }
    }

    public void DamagePlayer()
    {
        _health.Damage(damageAmount, gameObject, flickerDuration, invincibilityDuration, Vector3.zero);
    }
}
