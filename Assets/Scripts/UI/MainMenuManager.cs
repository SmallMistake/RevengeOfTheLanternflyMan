using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Tooltip("All pages to be controlled by menu controler")]
    public List<GameObject> allPages;
    [Tooltip("Optional way to set first page that is active when menu starts")]
    public GameObject firstPage;
    [Tooltip("Stack that tracks the pages that have been activated. Allows for them to slowly be exited.")]
    private Stack<GameObject> pageQueue = new Stack<GameObject>();
    [Tooltip("variable that tracks current page")]
    private GameObject currentPage;


    void Start()
    {
        foreach(var page in allPages)
        {
            page.SetActive(false);
        }
        if(firstPage != null)
        {
            ChangePage(firstPage);
        }
    }

    /// <summary>
    /// Call this when you want to go to a specific page
    /// </summary>
    /// <param name="nextPage"></param>
    public void ChangePage(GameObject nextPage)
    {
        currentPage?.SetActive(false);
        pageQueue.Push(currentPage);
        currentPage = nextPage;
        currentPage.SetActive(true);
    }

    /// <summary>
    /// Call this when you want to leave this page and return to the previous one.
    /// </summary>
    public void ReverseToPreviousPage()
    {
        if (pageQueue.Count > 0)
        {
            currentPage?.SetActive(false);
            currentPage = pageQueue.Pop();
            currentPage.SetActive(true);
        }
    }
}
