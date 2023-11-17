using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupFunctionsHelper : MonoBehaviour
{
    private void Awake()
    {
        SetupAreaCurrencies();
    }
    public void SetupAreaCurrencies()
    {
        AreaSpecificCurrency[] areaCurrency = GameObject.FindObjectsOfType<AreaSpecificCurrency>();
        int i = 0;
        foreach(AreaSpecificCurrency areaCurrencyItem in areaCurrency)
        {
            areaCurrencyItem.CurrencyID = i++;
        }
        print($"{i} Coins Setup");
    }
}
