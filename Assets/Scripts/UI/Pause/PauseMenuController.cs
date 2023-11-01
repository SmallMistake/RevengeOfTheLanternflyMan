using MoreMountains.InventoryEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>();

    private int currentPage;

    public Animator descriptionAnimator;
    public TextMeshProUGUI descriptionTextMesh;
    public TextMeshProUGUI pageTextMesh;

    void OnEnable()
    {
        currentPage = 0;
        bool firstPage = true;
        foreach (GameObject page in pages) { 
            if(firstPage)
            {
                firstPage = false;
                page.SetActive(true);
                ChangePageText(page.name);
                ChangeDescriptionText("");
            }
            else
            {
                page.SetActive(false);
            }
        }
        
    }

    public void ChangePage(int change)
    {
        currentPage +=  change;
        if(currentPage >= pages.Count)
        {
            currentPage = 0;
        }
        else if(currentPage < 0) { 
            currentPage = pages.Count - 1;
        }
        for(int i = 0; i < pages.Count; i++)
        {
            if(i == currentPage)
            {
                pages[i].SetActive(true);
                ChangePageText(pages[i].name);
            } else
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void ChangePageText(string pageText)
    {
        pageTextMesh.text = pageText;
    }

    public void ChangeDescriptionText(string descriptionText)
    {
        descriptionTextMesh.text = descriptionText;
        if(descriptionText == "")
        {
            descriptionAnimator.SetTrigger("Hide");
        }
        else
        {
            descriptionAnimator.SetTrigger("Reveal");
        }
    }
}
