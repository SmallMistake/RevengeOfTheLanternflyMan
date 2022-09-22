using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralUpdateManager : MonoBehaviour
{
    public TextMeshProUGUI messageArea;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }
    public void ShowNotification(string message)
    {
        messageArea.text = message;
        animator.SetTrigger("ShowMessage");
    }
}
