using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    ///
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Shield 2D")]
    public class CharacterShield2D : CharacterAbility
    {
        [Tooltip("the feedback to play when the Shield starts")]
        public MMFeedbacks ShieldStartFeedback;

        [Tooltip("the feedback to play when the Shield stops")]
        public MMFeedbacks ShieldStopFeedback;

        [Tooltip("the feedback to play when the Shield stops")]
        public List<CharacterHandleWeapon> WeaponSlots = new List<CharacterHandleWeapon>();

        protected CharacterButtonActivation _characterButtonActivation;

        protected const string _shieldingAnimationParameterName = "Shielding";
        protected int _shieldingAnimationParameter;

        private bool shieldButtonBuffered = false;

        /// <summary>
        /// On init we grab our components
        /// </summary>
        protected override void Initialization()
        {
            base.Initialization();
            _characterButtonActivation = _character?.FindAbility<CharacterButtonActivation>();
            ShieldStartFeedback?.Initialization(this.gameObject);
            ShieldStopFeedback?.Initialization(this.gameObject);
        }

        /// <summary>
        /// On HandleInput we watch for climb input and attachedToLadderIfSo
        /// </summary>
        protected override void HandleInput()
        {
            base.HandleInput();
            // if movement is prevented, or if the character is dead/frozen/can't move, we exit and do nothing
            if (!AbilityAuthorized
                || (
                    _condition.CurrentState != CharacterStates.CharacterConditions.Normal
                )
            )
            {
                return;
            }
            if (_inputManager.ShieldButton.State.CurrentState == MMInput.ButtonStates.ButtonDown )
            {
                shieldButtonBuffered = true;
            } else if (_inputManager.ShieldButton.State.CurrentState == MMInput.ButtonStates.ButtonUp)
            {
                shieldButtonBuffered = false;
            } else if(_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                StopShielding();
            }
        }

        public override void ProcessAbility()
        {
            if (_movement.CurrentState == CharacterStates.MovementStates.Idle || _movement.CurrentState == CharacterStates.MovementStates.Walking)
            {

                if(shieldButtonBuffered)
                {
                    bool isCharacterAttacking = false;
                    foreach (CharacterHandleWeapon weaponSlot in WeaponSlots)
                    {
                        if(weaponSlot.CurrentWeapon != null && weaponSlot.CurrentWeapon.WeaponState.CurrentState == Weapon.WeaponStates.WeaponUse)
                        {
                            isCharacterAttacking = true;
                            break;
                        }
                    }
                    if(!isCharacterAttacking) {
                        _movement.ChangeState(CharacterStates.MovementStates.Shielding);
                    } 
                }
            } 
            else if (_movement.CurrentState == CharacterStates.MovementStates.Shielding)
            {
                if(!shieldButtonBuffered)
                {
                    _movement.ChangeState(CharacterStates.MovementStates.Idle);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void StartShielding()
        {
            _movement.ChangeState(CharacterStates.MovementStates.Shielding);
            ShieldStartFeedback?.PlayFeedbacks(this.transform.position);
            PlayAbilityStartFeedbacks();
            /*
            MMCharacterEvent.Trigger(_character, MMCharacterEventTypes.Jump);
            */
        }

        /// <summary>
        /// Stops the jump
        /// </summary>
        public virtual void StopShielding()
        {
            _movement.ChangeState(CharacterStates.MovementStates.Idle);
            ShieldStopFeedback?.PlayFeedbacks(this.transform.position);
            StopStartFeedbacks();
            PlayAbilityStopFeedbacks();
        }

        /// <summary>
        /// Adds required animator parameters to the animator parameters list if they exist
        /// </summary>
        protected override void InitializeAnimatorParameters()
        {
            RegisterAnimatorParameter(_shieldingAnimationParameterName, AnimatorControllerParameterType.Bool, out _shieldingAnimationParameter);
        }

        /// <summary>
        /// At the end of each cycle, sends Jumping states to the Character's animator
        /// </summary>
        public override void UpdateAnimator()
        {
            MMAnimatorExtensions.UpdateAnimatorBool(_animator, _shieldingAnimationParameter, (_movement.CurrentState == CharacterStates.MovementStates.Shielding), _character._animatorParameters, _character.RunAnimatorSanityChecks);
        }
    }
}