using IntronDigital;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyTracker : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currencyTextMesh;
    //TODO expand this to be able to auto update when currency change event occurs
    private void OnEnable()
    {
        currencyTextMesh.text = $"{ProgressManager.Instance.commonCurrency}";
    }
}
