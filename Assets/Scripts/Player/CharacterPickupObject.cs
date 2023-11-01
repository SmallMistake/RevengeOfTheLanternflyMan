using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickupObject : CharacterAbility
{

    [Tooltip("Object Holder Object that represents where the object is physically held. Usually a child in object")]
    [SerializeField]
    private ObjectHolder objectHolder;

    public CharacterHandleWeapon characterWeaponHandler;
    public CharacterHandleWeapon characterWeaponHandlerSecondary;
    public GameObject weaponAttachment;

    protected const string _carryingObjectAnimationParameterName = "CarryingObject";
    protected int _carryingObjectAnimationParameter;

    /// <summary>
    /// On init we grab our components
    /// </summary>
    protected override void Initialization()
    {
        base.Initialization();
    }

    /// <summary>
    /// On HandleInput we watch for jump input and trigger a jump if needed
    /// </summary>
    protected override void HandleInput()
    {
        base.HandleInput();
        //Listen for input and make desision;
        // if movement is prevented, or if the character is dead/frozen/can't move, we exit and do nothing
        if (!AbilityAuthorized || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
        {
            return;
        }
        if (_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            bool? holdingObject = objectHolder.HandleButtonPress();
            if (holdingObject == null)
            {}
            else if (holdingObject == true) { 
                _movement.ChangeState(CharacterStates.MovementStates.CarryingObject);
                HideWeaponVisuals();
            } else
            {
                _movement.ChangeState(CharacterStates.MovementStates.Idle);
                ShowWeaponVisuals();
            }
        }
        else
        {
            //Try to use held object
            if (_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonDown || _inputManager.ShootAxis == MMInput.ButtonStates.ButtonDown)
            {
                if (objectHolder.TryToUse())
                {

                }
            }
        }
    }

    private void HideWeaponVisuals()
    {
        characterWeaponHandler.AbilityPermitted = false;
        characterWeaponHandlerSecondary.AbilityPermitted = false;
        weaponAttachment.SetActive(false);
    }

    private void ShowWeaponVisuals()
    {
        characterWeaponHandler.AbilityPermitted = true;
        characterWeaponHandlerSecondary.AbilityPermitted = true;
        weaponAttachment.SetActive(true);
    }

    /// <summary>
    /// Adds required animator parameters to the animator parameters list if they exist
    /// </summary>
    protected override void InitializeAnimatorParameters()
    {
        RegisterAnimatorParameter(_carryingObjectAnimationParameterName, AnimatorControllerParameterType.Bool, out _carryingObjectAnimationParameter);
    }

    /// <summary>
    /// Sends the current speed and the current value of the Walking state to the animator
    /// </summary>
    public override void UpdateAnimator()
    {
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, _carryingObjectAnimationParameter, (_movement.CurrentState == CharacterStates.MovementStates.CarryingObject), _character._animatorParameters, _character.RunAnimatorSanityChecks);
    }
}
