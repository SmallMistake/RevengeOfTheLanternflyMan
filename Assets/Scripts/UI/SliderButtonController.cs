using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// This is added to a button that is supposed to act as a slider
/// </summary>
public class SliderButtonController : MonoBehaviour
{
    [Header("Functional Parts")]
    [Tooltip("Amount for slider to change by")]
    public float stepAmount;
    [Tooltip("SLider to control")]
    public Slider slider;
    [Tooltip("When this button is selected, listen for increase or decrease button presses")]
    public Button buttonToListenTo;
    [Tooltip("UI Navigation Input Reference")]
    public InputActionReference navigationActionReference;

    [Header("Visual Elements")]
    [Tooltip("Add a text mesh here to have it update as the slider updates")]
    public TextMeshProUGUI optionalTextMesh;
    [Tooltip("If button is held, increment value")]
    private Coroutine buttonIncreaseHeldDownCoroutine;

    [Header("Events")]
    [SerializeField]
    [Tooltip("On value change, call these events")]
    private UnityEvent<float> OnValueChange;



    private void OnEnable()
    {
        navigationActionReference.action.performed += CheckIfNeedToIncrease;
        if (optionalTextMesh != null)
        {
            slider.onValueChanged.AddListener(HandleValueChange);
            UpdateTextMesh(slider.value);
        }
    }

    private void OnDisable()
    {
        navigationActionReference.action.performed -= CheckIfNeedToIncrease;
        if (optionalTextMesh != null)
        {
            slider.onValueChanged.RemoveListener(HandleValueChange);
        }
    }

    private void CheckIfNeedToIncrease(InputAction.CallbackContext actionContext)
    {
        if(buttonToListenTo.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            Vector2 movementAmount = actionContext.action.ReadValue<Vector2>();
            if(buttonIncreaseHeldDownCoroutine != null)
            {
                StopCoroutine(buttonIncreaseHeldDownCoroutine);
            }
            if (movementAmount.x > 0 )
            {
                buttonIncreaseHeldDownCoroutine = StartCoroutine(IncreaseSlider(stepAmount));
            } else if (movementAmount.x < 0)
            {
                buttonIncreaseHeldDownCoroutine = StartCoroutine(IncreaseSlider(-stepAmount));
            }
        }
    }

    IEnumerator IncreaseSlider(float valueToIncreaseBy)
    {
        while(true)
        {
            slider.value += valueToIncreaseBy;
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }


    private void HandleValueChange(float newValue)
    {
        OnValueChange?.Invoke(newValue);
        UpdateTextMesh(newValue);
    }

    private void UpdateTextMesh(float value)
    {
        int displayValue = Convert.ToInt32(value * 100);
        optionalTextMesh.text = $"{displayValue}%";
    }
}
