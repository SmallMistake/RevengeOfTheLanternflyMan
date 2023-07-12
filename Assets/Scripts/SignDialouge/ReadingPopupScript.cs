using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingPopupScript : MonoBehaviour
{
    public Transform textHolder; //Where the text Object is added;
    public GameObject popupObject;
    private bool popupOpen = false;
    private bool cooldownActive = false;
    private bool popupChangedThisFrame = false; //Prevents popup from being closed the same frame it was opened

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SignScript.SignInteractedWith += showPopup;
    }

    private void OnDestroy()
    {
        SignScript.SignInteractedWith -= showPopup;
    }

    public void showPopup(GameObject textObject)
    {
        if (!cooldownActive && !popupOpen)
        {
            
            foreach (Transform child in textHolder)
            {
                GameObject.Destroy(child.gameObject);
            }
            popupObject.SetActive(true);
            GameObject createdTextObject = Instantiate(textObject, transform.position, transform.rotation);
            createdTextObject.transform.SetParent(textHolder);
            RectTransform rectTransform = createdTextObject.GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            rectTransform.offsetMax = new Vector2(0, -0); // new Vector2(-right, -top);
            createdTextObject.transform.localScale = new Vector3(1, 1, 1);
            popupOpen = true;
            Time.timeScale = 0;
            popupChangedThisFrame = true;
            animator.SetTrigger("ShowMessage");
        }
    }


    private void LateUpdate()
    {
        if (!popupChangedThisFrame && popupOpen && (Input.GetButtonDown("Primary") || Input.GetButtonDown("Select")))
        {
            animator.SetTrigger("HideMessage");
            popupChangedThisFrame = true;
            popupOpen = false;
        }
        else
        {
            popupChangedThisFrame = false;
        }
    }

    public void MessageClosed()
    {
        Time.timeScale = 1;
        popupObject.SetActive(false);
    }
}
