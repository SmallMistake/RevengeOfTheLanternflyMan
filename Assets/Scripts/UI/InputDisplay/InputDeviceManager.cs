using BehaviorDesigner.Runtime.Tasks;
using IntronDigital;
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
[CreateAssetMenu(fileName = "InputManager", menuName = "InputDeviceManager", order = 0)]
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
        InputAction action = _playerInput.actions[actionName];
        InputBinding deviceBinding = action.bindings[(int)deviceType];
        return deviceBinding;
    }
}
