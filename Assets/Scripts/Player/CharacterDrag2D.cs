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
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Drag 2D")]
    public class CharacterDrag2D : CallableCharacterAbility
    {
        [Tooltip("If your character uses an orientation module use this to turn it off while dragging")]
        public CharacterOrientation2D characterOrientation2DController;

        [Tooltip("The area that represents where a player can grab objects")]
        public DragPointController dragPointController;

        [Tooltip("the feedback to play when the drag starts")]
        public MMFeedbacks DragStartFeedback;

        [Tooltip("the feedback to play when the drag stops")]
        public MMFeedbacks DragStopFeedback;

        protected CharacterButtonActivation _characterButtonActivation;

        protected const string _draggingObjectAnimationParameterName = "Dragging";
        protected int _draggingAnimationParameter;


        /// <summary>
        /// On init we grab our components
        /// </summary>
        protected override void Initialization()
        {
            base.Initialization();
            _characterButtonActivation = _character?.FindAbility<CharacterButtonActivation>();
            DragStartFeedback?.Initialization(this.gameObject);
            DragStopFeedback?.Initialization(this.gameObject);
        }

        //If button up stop Dragging
        protected override void HandleInput()
        {
            if(_movement.CurrentState == CharacterStates.MovementStates.Dragging)
            {
                if (_inputManager.InteractButton.State.CurrentState == MMInput.ButtonStates.ButtonUp)
                {
                    StopDrag();
                }
            }
        }
        public override bool IsAbilityActivatable()
        {
            // if movement is prevented, or if the character is dead/frozen/can't move, we exit and do nothing
            if (!AbilityAuthorized
                || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
            )
            {
                return false;
            }
            if (_movement.CurrentState == CharacterStates.MovementStates.Idle)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// On Button Press, check if something draggable is in range
        /// </summary>
        public override void Activate()
        {
            if(dragPointController.CanDrag())
            {
                StartDrag();
            }
        }

        /// <summary>
        /// Put the character on the climbable
        /// </summary>
        public virtual void StartDrag()
        {
            dragPointController.StartDragging();
            //gameObject.transform.position = climbableInRange.transform.position;
            _movement.ChangeState(CharacterStates.MovementStates.Dragging);
            DragStartFeedback?.PlayFeedbacks(this.transform.position);
            PlayAbilityStartFeedbacks();
            characterOrientation2DController.PermitAbility(false);
        }

        /// <summary>
        /// Stops the Climb
        /// </summary>
        public virtual void StopDrag()
        {
            characterOrientation2DController.PermitAbility(true);
            dragPointController.StopDragging();
            _movement.ChangeState(CharacterStates.MovementStates.Idle);
            //gameObject.transform.position = climbableInRange.transform.position + climbableInRange.GetExitOffset();
            DragStopFeedback?.PlayFeedbacks(this.transform.position);
            StopStartFeedbacks();
            PlayAbilityStopFeedbacks();
        }

        protected override void InitializeAnimatorParameters()
        {
            RegisterAnimatorParameter(_draggingObjectAnimationParameterName, AnimatorControllerParameterType.Bool, out _draggingAnimationParameter);
        }

        /// <summary>
        /// Sends the current speed and the current value of the Walking state to the animator
        /// </summary>
        public override void UpdateAnimator()
        {
            MMAnimatorExtensions.UpdateAnimatorBool(_animator, _draggingAnimationParameter, (_movement.CurrentState == CharacterStates.MovementStates.Dashing), _character._animatorParameters, _character.RunAnimatorSanityChecks);
        }
    }
}