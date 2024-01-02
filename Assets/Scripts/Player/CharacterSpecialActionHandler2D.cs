using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
	/// An event triggered every time the current Special Use changes, for other classes to listen to
	/// </summary>
	public struct SpecialUseChangeEvent
    {
        public CharacterSpecialActionHandler2D AffectedHandler;
        public CallableCharacterAbility NewCurrentAbility;

        public SpecialUseChangeEvent(CharacterSpecialActionHandler2D affectedHandler, CallableCharacterAbility newCurrentAbility)
        {
            AffectedHandler = affectedHandler;
            NewCurrentAbility = newCurrentAbility;
        }

        static SpecialUseChangeEvent e;
        public static void Trigger(CharacterSpecialActionHandler2D affectedHandler, CallableCharacterAbility newCurrentAbility)
        {
            e.AffectedHandler = affectedHandler;
            e.NewCurrentAbility = newCurrentAbility;
            MMEventManager.TriggerEvent(e);
        }
    }

    /// <summary>
	/// An event triggered every time the a Special is Used , for other classes to listen to
	/// </summary>
	public struct SpecialUsedEvent
    {

        public SpecialUsedEvent(bool used)
        { }

        static SpecialUsedEvent e;
        public static void Trigger()
        {
            MMEventManager.TriggerEvent(e);
        }
    }

    /// <summary>
    /// This ability controls and calls all actions that are listed below
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Special Action Handler")]
    public class CharacterSpecialActionHandler2D : CharacterAbility
    {
        // Go Through a list of abilities to find one that can be activated
        public List<CallableCharacterAbility> abilitiesInOrderOfActivation = new List<CallableCharacterAbility>();

        //This is filled to show the ability that will be called if the button is pressed
        [SerializeField]
        private CallableCharacterAbility nextAbility;
        // This is filled with an ability if it is still active
        [SerializeField]
        private CallableCharacterAbility activeAbility;
        /// <summary>
        /// On init we grab our components
        /// </summary>
        protected override void Initialization()
        {
            base.Initialization();
        }

        /// <summary>
        /// On HandleInput we check if our character is in a position to use a special ability and then we go down the list to check if one is available for use.
        /// </summary>
        protected override void HandleInput()
        {
            base.HandleInput();

            FindCurrentAvailableAction();

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

        private void FindCurrentAvailableAction()
        {
            if(activeAbility != null)
            {
                if (!activeAbility.IsStillActive())
                {
                    activeAbility = null;
                }
            }
            foreach (CallableCharacterAbility ability in abilitiesInOrderOfActivation)
            {
                if (ability.IsAbilityActivatable())
                {
                    if (nextAbility != ability)
                    {
                        nextAbility = ability;
                        SpecialUseChangeEvent.Trigger(this, ability);
                    }
                    break;
                }
            }
        }
        /// <summary>
        /// On Button Press, check if there is a climbable point in range and if so, start or stop climbing
        /// </summary>
        public void HandleButtonPress()
        {
            if(activeAbility == null)
            {
                nextAbility.Activate();
                activeAbility = nextAbility;
            }
            else
            {
                activeAbility.Activate();
                activeAbility = null;
            }
        }
    }
}