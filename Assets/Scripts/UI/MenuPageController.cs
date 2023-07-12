using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPageController : MonoBehaviour
{
    public Button firstSelectedButton;

    private GameObject previouslySelectedButton;

    private void OnEnable()
    {
        if(previouslySelectedButton != null){
            EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
        }
    }

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            previouslySelectedButton = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        }

        if (Input.GetButtonDown("Select") || Input.GetButtonDown("Start") || Input.GetButtonDown("Primary"))
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}
