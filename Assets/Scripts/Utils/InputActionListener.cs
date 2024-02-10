using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
/// <summary>
/// This listner can have a input reference added to send out events when specific input events occur.
/// </summary>
public class InputActionListener : MonoBehaviour
{
    [SerializeField]
    private InputActionReference inputActionToListenFor;

    [SerializeField]
    private UnityEvent onActionPerformed;

    private void OnEnable()
    {
        inputActionToListenFor.action.performed += OnActionPerformed;
    }

    private void OnDisable()
    {
        inputActionToListenFor.action.performed -= OnActionPerformed;
    }

    private void OnActionPerformed(InputAction.CallbackContext actionContext) {
        onActionPerformed?.Invoke();
    }
}
