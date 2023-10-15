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
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Climb 2D")]
    public class CharacterClimb2D : CharacterAbility
    {
        [Tooltip("the feedback to play when the climb starts")]
        public MMFeedbacks ClimbStartFeedback;

        [Tooltip("the feedback to play when the climb stops")]
        public MMFeedbacks ClimbStopFeedback;

        protected CharacterButtonActivation _characterButtonActivation;

        [Tooltip("the script that is attacted to the interaction trigger area to get the climb point the player is going to attach to")]
        [SerializeField]
        private CharacterClimbingInteractor characterClimbingInteractor;

        //protected const string _jumpingAnimationParameterName = "Jumping";
        //protected const string _hitTheGroundAnimationParameterName = "HitTheGround";
        //protected int _jumpingAnimationParameter;
        //protected int _hitTheGroundAnimationParameter;

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

        /// <summary>
        /// On HandleInput we watch for climb input and attachedToLadderIfSo
        /// </summary>
        protected override void HandleInput()
        {
            base.HandleInput();
            // if movement is prevented, or if the character is dead/frozen/can't move, we exit and do nothing
            if (!AbilityAuthorized
                || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
            {
                return;
            }
            if (_inputManager.InteractButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                HandleButtonPress();
            }
        }
        public void HandleButtonPress()
        {
            ClimbablePoint climbableInRange = characterClimbingInteractor.GetClimbableInRange();
            if (climbableInRange != null)
            {
                 if (_movement.CurrentState != CharacterStates.MovementStates.Climbing)
                {
                    print("Start Climb");
                    StartClimb(climbableInRange);
                }
                else
                {
                    print("Stop Climb");
                    StopClimb(climbableInRange);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void StartClimb(ClimbablePoint climbableInRange)
        {
            gameObject.transform.position = climbableInRange.transform.position;
            _movement.ChangeState(CharacterStates.MovementStates.Climbing);
            ClimbStartFeedback?.PlayFeedbacks(this.transform.position);
            PlayAbilityStartFeedbacks();

            /*
            MMCharacterEvent.Trigger(_character, MMCharacterEventTypes.Jump);
            */
        }

        /// <summary>
        /// Stops the jump
        /// </summary>
        public virtual void StopClimb(ClimbablePoint climbableInRange)
        {
            _movement.ChangeState(CharacterStates.MovementStates.Idle);
            gameObject.transform.position = climbableInRange.transform.position + climbableInRange.GetExitOffset();
            ClimbStopFeedback?.PlayFeedbacks(this.transform.position);
            StopStartFeedbacks();
            PlayAbilityStopFeedbacks();
        }

        /// <summary>
        /// Adds required animator parameters to the animator parameters list if they exist
        /// </summary>
        protected override void InitializeAnimatorParameters()
        {
            //RegisterAnimatorParameter(_jumpingAnimationParameterName, AnimatorControllerParameterType.Bool, out _jumpingAnimationParameter);
            //RegisterAnimatorParameter(_hitTheGroundAnimationParameterName, AnimatorControllerParameterType.Bool, out _hitTheGroundAnimationParameter);
        }

        /// <summary>
        /// At the end of each cycle, sends Jumping states to the Character's animator
        /// </summary>
        public override void UpdateAnimator()
        {
            //MMAnimatorExtensions.UpdateAnimatorBool(_animator, _jumpingAnimationParameter, (_movement.CurrentState == CharacterStates.MovementStates.Jumping), _character._animatorParameters, _character.RunAnimatorSanityChecks);
            //MMAnimatorExtensions.UpdateAnimatorBool(_animator, _hitTheGroundAnimationParameter, _controller.JustGotGrounded, _character._animatorParameters, _character.RunAnimatorSanityChecks);
        }
    }
}