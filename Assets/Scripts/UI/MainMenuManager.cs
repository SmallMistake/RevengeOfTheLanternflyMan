using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
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
}
