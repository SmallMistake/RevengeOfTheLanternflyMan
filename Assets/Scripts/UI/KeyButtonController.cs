using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyButtonController : MonoBehaviour
{
    public OnscreenKeyboardController keyboardController;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { keyboardController.KeyPressed(GetComponentInChildren<TextMeshProUGUI>().text); });
    }
}
