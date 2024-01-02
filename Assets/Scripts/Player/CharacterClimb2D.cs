using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// This ability allows the character to climb objects
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Climb 2D")]
    public class CharacterClimb2D : CallableCharacterAbility
    {
        [Tooltip("the feedback to play when the climb starts")]
        public MMFeedbacks ClimbStartFeedback;

        [Tooltip("the feedback to play when the climb stops")]
        public MMFeedbacks ClimbStopFeedback;

        protected CharacterButtonActivation _characterButtonActivation;

        [Tooltip("the script that is attacted to the interaction trigger area to get the climb point the player is going to attach to")]
        [SerializeField]
        private CharacterClimbingInteractor characterClimbingInteractor;

        /// <summary>
        /// On init we grab our components
        /// </summary>
        protected override void Initialization()
        {
            base.Initialization();
            _characterButtonActivation = _character?.FindAbility<CharacterButtonActivation>();
            ClimbStartFeedback?.Initialization(this.gameObject);
            ClimbStopFeedback?.Initialization(this.gameObject);
        }

        public override bool IsAbilityActivatable()
        {
            // if movement is prevented, or if the character is dead/frozen/can't move, we exit and do nothing
            if (!AbilityAuthorized
                || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
            {
                return false;
            }
            if (_inputManager.InteractButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                return true;
            }
            return false;
        }

        public override void Activate()
        {
            HandleButtonPress();
        }

        /// <summary>
        /// On Button Press, check if there is a climbable point in range and if so, start or stop climbing
        /// </summary>
        public void HandleButtonPress()
        {
            ClimbablePoint climbableInRange = characterClimbingInteractor.GetClimbableInRange();
            if (climbableInRange != null)
            {
                 if (_movement.CurrentState != CharacterStates.MovementStates.Climbing)
                {
                    StartClimb(climbableInRange);
                }
                else
                {
                    StopClimb(climbableInRange);
                }
            }
        }

        /// <summary>
        /// Put the character on the climbable
        /// </summary>
        public virtual void StartClimb(ClimbablePoint climbableInRange)
        {
            gameObject.transform.position = climbableInRange.transform.position;
            _movement.ChangeState(CharacterStates.MovementStates.Climbing);
            ClimbStartFeedback?.PlayFeedbacks(this.transform.position);
            PlayAbilityStartFeedbacks();

            /* TODO make this send out a climb trigger event
            MMCharacterEvent.Trigger(_character, MMCharacterEventTypes.Jump);
            */
        }

        /// <summary>
        /// Stops the Climb
        /// </summary>
        public virtual void StopClimb(ClimbablePoint climbableInRange)
        {
            _movement.ChangeState(CharacterStates.MovementStates.Idle);
            gameObject.transform.position = climbableInRange.transform.position + climbableInRange.GetExitOffset();
            ClimbStopFeedback?.PlayFeedbacks(this.transform.position);
            StopStartFeedbacks();
            PlayAbilityStopFeedbacks();
        }
    }
}