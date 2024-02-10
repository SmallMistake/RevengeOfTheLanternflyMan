using BehaviorDesigner.Runtime.Tasks;
using MoreMountains.Tools;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;

/// <summary>
/// Provides device data to listeners
/// </summary>
public class InputDeviceManager : MMSingleton<InputDeviceManager>
{
    private DynamicDeviceType activeDevice = DynamicDeviceType.Keyboard;
    [SerializeField]
    private PlayerInput _playerInput;

    // Event Fired when the active device changes
    public event System.Action ActiveDeviceChangeEvent;

    protected override void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
        }
        _playerInput = GameObject.FindGameObjectWithTag("InputManager").GetComponent<PlayerInput>();
        InputSystem.onActionChange += TrackActions;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= TrackActions;
    }

    public PlayerInput GetPlayerInput()
    {
        return _playerInput;
    }

    public DynamicDeviceType GetActiveDevice()
    {
        return activeDevice;
    }

    private void TrackActions(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputAction inputAction = (InputAction)obj;
            InputControl activeControl = inputAction.activeControl;
            //Debug.LogFormat("Current Control {0}", activeControl);

            var newDevice = DynamicDeviceType.Keyboard;

            if (activeControl.device is Keyboard)
            {
                newDevice = DynamicDeviceType.Keyboard;
            }

            else if (activeControl.device is Gamepad)
            {
                newDevice = DynamicDeviceType.Gamepad;
                //Further Expand controller brands by adding them here
                if(activeControl.device is SwitchProControllerHID)
                {
                    newDevice = DynamicDeviceType.Switch;
                }
            }

            // we detected a device change
            if(activeDevice != newDevice)
            {
                activeDevice = newDevice;
                ActiveDeviceChangeEvent?.Invoke();
            }
        }
    }

    public InputBinding GetBinding(string actionName, DynamicDeviceType deviceType, PlayerInput _playerInput)
    {
        string[] actionParts = actionName.Split('/');
        string direction = "";
        // Handle Composte Actions
        if (actionParts.Length >= 3 ) { 
            direction = actionParts[2];
            actionName = $"{actionParts[0]}/{actionParts[1]}";
        }
        int bindingKey = (int)deviceType;
        // Handle Keyboard Composite
        switch (direction)
        {
            case "Up":
                bindingKey += 1;
                break;
            case "Down":
                bindingKey += 2;
                break;
            case "Left":
                bindingKey += 3;
                break;
            case "Right":
                bindingKey += 4;
                break;
        }

        InputAction action = _playerInput.actions[actionName];
        InputBinding deviceBinding = action.bindings[bindingKey];
        return deviceBinding;
    }
}
