using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject mainPage;
    public GameObject creditsPage;

    private GameObject currentMenu;

    void Start()
    {
        mainPage.SetActive(true);
        creditsPage.SetActive(false);
        currentMenu = mainPage;
    }

    public void ChangePage(GameObject nextPage)
    {

        currentMenu.SetActive(false);
        currentMenu = nextPage;
        currentMenu.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Select") || Input.GetButtonDown("Start") || Input.GetButtonDown("Primary"))
        {
           EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}
