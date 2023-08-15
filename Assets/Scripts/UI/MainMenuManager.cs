using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public GameObject mainPage;
    public GameObject saveFilePage;
    public GameObject settingsPage;
    public GameObject creditsPage;

    private GameObject currentMenu;

    void Start()
    {
        mainPage.SetActive(true);
        saveFilePage.SetActive(false);
        settingsPage.SetActive(false);
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
